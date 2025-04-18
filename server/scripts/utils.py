import requests
import matplotlib.pyplot as plt
from typing import TypeAlias, Tuple, Any
import hashlib


def get_stable_id(x: float, y: float,
                  zLevel: int, precision: int = 10_000_000_000_000_000) -> str:
    ix = int(x * precision)
    iy = int(y * precision)
    return hashlib.sha256(f"{ix},{iy},{zLevel}".encode()).hexdigest()


# Coordinate is a [x, y, zLevel]
Coordinate: TypeAlias = Tuple[float, float, int]


def get_directions(origin: Coordinate, dest: Coordinate) -> None:
    origin_str = str(origin[0]) + "," + str(origin[1]) + "," + str(origin[2])
    dest_str = str(dest[0]) + "," + str(dest[1]) + "," + str(dest[2])

    r = requests.get(
      'https://api.mapsindoors.com/ntuprod/api/directions/NTU_Graph?origin=' +
      origin_str +
      '&destination=' +
      dest_str +
      '&mode=WALKING&lr=en-US'
    )
    return r.json()


def get_directions_avoid_stairs(origin: Coordinate, dest: Coordinate) -> None:
    origin_str = str(origin[0]) + "," + str(origin[1]) + "," + str(origin[2])
    dest_str = str(dest[0]) + "," + str(dest[1]) + "," + str(dest[2])

    r = requests.get(
      'https://api.mapsindoors.com/ntuprod/api/directions/NTU_Graph?origin=' +
      origin_str +
      '&destination=' +
      dest_str +
      '&mode=WALKING&avoid=stairs&lr=en-US&'
    )
    return r.json()


def search_data(data: Any, name: str) -> Any:
    for item in data:
        if name == item["name"]:
            return item
    return None


def plot_directions(origin: Coordinate, dest: Coordinate) -> list:
    """
        plots path from origin to destination
    """
    response = get_directions(origin, dest)
    path_l1, path_l2, path_l3, path_l4, path_l5 = [], [], [], [], []

    for item in response["routes"][0]["legs"]:
        for step in item["steps"]:
            for coord in step["geometry"]:
                if coord["floor_name"] == "L5":
                    path_l5.append([coord["lng"], coord["lat"]])
                elif coord["floor_name"] == "L4":
                    path_l4.append([coord["lng"], coord["lat"]])
                elif coord["floor_name"] == "L3":
                    path_l3.append([coord["lng"], coord["lat"]])
                elif coord["floor_name"] == "L2":
                    path_l2.append([coord["lng"], coord["lat"]])
                elif coord["floor_name"] == "L1":
                    path_l1.append([coord["lng"], coord["lat"]])

    to_plot = [0 for i in range(5)]

    if len(path_l1) > 0:
        xp1, yp1 = zip(*path_l1)
        plt.plot(xp1, yp1, color="red")
        to_plot[0] = 1
    if len(path_l2) > 0:
        xp2, yp2 = zip(*path_l2)
        plt.plot(xp2, yp2, color="green")
        to_plot[1] = 1
    if len(path_l3) > 0:
        xp3, yp3 = zip(*path_l3)
        plt.plot(xp3, yp3, color="blue")
        to_plot[2] = 1
    if len(path_l4) > 0:
        xp4, yp4 = zip(*path_l4)
        plt.plot(xp4, yp4, color="purple")
        to_plot[3] = 1
    if len(path_l5) > 0:
        xp5, yp5 = zip(*path_l5)
        plt.plot(xp5, yp5, color="orange")
        to_plot[4] = 1

    return to_plot
