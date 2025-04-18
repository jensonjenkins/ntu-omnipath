import logging
import json
from typing import List, Tuple
from scripts.utils import search_data, get_directions_avoid_stairs, Coordinate, get_stable_id
from flask import Blueprint, jsonify, request

logging.basicConfig(
        level=logging.INFO,
        format="%(asctime)s [%(levelname)s] - [%(filename)s > %(funcName)s() > %(lineno)s] - %(message)s",
        datefmt="%d/%m/%Y %H:%M:%S"
        )

lift_bp = Blueprint("lift_bp", __name__)


@lift_bp.route("/get-lift", methods=["GET", "POST"])
def lift_route():
    if request.method == "GET":
        return jsonify({"status": "OK"}), 200
    elif request.method == "POST":
        return lift(request.get_json(force=True))


def lift(payload):
    logging.info("Opening north_spine_locations.json...")
    file = open("./data/north_spine_locations.json", "r")
    ns_data = json.load(file)
    file.close()

    logging.info("Opening north_spine_lifts.json...")
    file = open("./data/north_spine_lifts.json", "r")
    lifts_data = json.load(file)
    file.close()

    origin_str = payload["origin"]
    dest_str = payload["dest"]
    origin = search_data(ns_data, payload["origin"])
    dest = search_data(ns_data, payload["dest"])

    if origin is None:
        return jsonify(
                {"error": f"origin '{origin_str}' not found in data."}
                ), 400
    if dest is None:
        return jsonify(
                {"error": f"dest '{dest_str}' not found in data."}
                ), 400

    origin_c = (origin["latitude"], origin["longitude"], origin["level"])
    dest_c = (dest["latitude"], dest["longitude"], dest["level"])

    logging.info(f"Requesting NTU Maps API for {origin_str} to {dest_str}.")
    request = get_directions_avoid_stairs(origin_c, dest_c)

    legs = request["routes"][0]["legs"]

    lift_coordinates: List[Tuple[Coordinate, Coordinate]] = []

    for i in range(len(legs)):
        if legs[i]["leg_start_reason"] == "floor_change":
            floor_change_geometry = legs[i]["steps"][0]["geometry"]

            ss = floor_change_geometry[0]
            se = floor_change_geometry[1]
            print(ss)
            print(se)

            ss_hash = get_stable_id(ss["lat"], ss["lng"], int(ss["zLevel"]))
            se_hash = get_stable_id(se["lat"], se["lng"], int(se["zLevel"]))

            lift_pair = [-1, -1]
            for lift in lifts_data:
                if ss_hash == lift["hash"]:
                    lift_pair[0] = lift["name"]
                if se_hash == lift["hash"]:
                    lift_pair[1] = lift["name"]
            lift_coordinates.append(lift_pair)

    logging.info(lift_coordinates)

    return jsonify({"stair_coordinates": lift_coordinates}), 200
