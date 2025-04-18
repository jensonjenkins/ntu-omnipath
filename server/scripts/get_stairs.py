import json
from typing import List, Tuple
from utils import search_data, get_directions, Coordinate
from pprint import pp


def main() -> None:
    file = open("./data/north_spine_locations.json", "r")
    data = json.load(file)

    origin = search_data(data, "LT8 (NS)")
    dest = search_data(data, "LT9 (NS)")

    origin_c = (origin["latitude"], origin["longitude"], origin["level"])
    dest_c = (dest["latitude"], dest["longitude"], dest["level"])

    request = get_directions(origin_c, dest_c)

    legs = request["routes"][0]["legs"]

    stair_coordinates: List[Tuple[Coordinate, Coordinate]] = []

    for i in range(len(legs)):
        if legs[i]["leg_start_reason"] == "floor_change":
            floor_change_geometry = legs[i]["steps"][0]["geometry"]

            ss = floor_change_geometry[0]
            se = floor_change_geometry[1]

            ss_c = (ss["lat"], ss["lng"], ss["zLevel"])
            se_c = (se["lat"], se["lng"], se["zLevel"])
            stair_coordinates.append((ss_c, se_c))

    pp(stair_coordinates)

    file.close()


if __name__ == "__main__":
    main()
