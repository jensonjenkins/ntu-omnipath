import json
import matplotlib
import matplotlib.pyplot as plt
from utils import search_data, plot_directions, plot_ns


matplotlib.use('TkAgg')


def main() -> None:
    file = open("./data/north_spine_locations.json", "r")
    data = json.load(file)

    origin = search_data(data, "LT8 (NS)")
    dest = search_data(data, "LT9 (NS)")

    origin_c = (origin["latitude"], origin["longitude"], origin["level"])
    dest_c = (dest["latitude"], dest["longitude"], dest["level"])

    plt.figure(figsize=(6, 12))

    to_plot = plot_directions(origin_c, dest_c)
    plot_ns(to_plot)

    plt.show()
    file.close()


if __name__ == "__main__":
    main()
