import logging
import json
import math
from scripts.utils import search_data
from flask import Blueprint, jsonify, request

logging.basicConfig(level=logging.INFO,
                    format="%(asctime)s [%(levelname)s] - [%(filename)s > %(funcName)s() > %(lineno)s] - %(message)s",
                    datefmt="%d/%m/%Y %H:%M:%S")

toilet_bp = Blueprint("toilet_bp", __name__)


@toilet_bp.route("/get-toilet", methods=["GET", "POST"])
def toilet_route():
    if request.method == "GET":
        return jsonify({"service": "toilet", "status": "OK"}), 200
    elif request.method == "POST":
        return toilet(request.get_json(force=True))


def toilet(payload):
    logging.info("Opening north_spine_toilets.json...")
    file = open("./data/north_spine_toilets.json", "r")
    toilet_data = json.load(file)
    file.close()

    logging.info("Opening north_spine_locations.json...")
    file = open("./data/north_spine_locations.json", "r")
    ns_data = json.load(file)
    file.close()

    origin_str = payload["origin"]
    origin = search_data(ns_data, payload["origin"])

    if origin is None:
        return jsonify(
                {"error": f"origin '{origin_str}' not found in data."}
                ), 400
    try:
        gender_str = payload["gender"]
    except KeyError:
        return jsonify(
                {"error": "Please specify gender in payload."}
                ), 400

    origin_c = (origin["latitude"], origin["longitude"], origin["level"])

    dist = float('inf')
    nearest_toilet = ""

    for toilet in toilet_data:
        toilet_c = (toilet["latitude"], toilet["longitude"], origin["level"])
        dx_sqr = (origin_c[0] - toilet_c[0])**2
        dy_sqr = (origin_c[1] - toilet_c[1])**2
        dz_sqr = (origin_c[2] - toilet_c[2])**2
        cur_dist = math.sqrt(dx_sqr + dy_sqr + dz_sqr)

        if cur_dist < dist and gender_str == toilet["gender"]:
            dist = cur_dist
            nearest_toilet = toilet["name"]

    return jsonify({"nearest_toilet": nearest_toilet}), 200
