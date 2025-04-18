# main.py

import json


def main():
    file = open("./data/north_spine_locations.txt", "r")
    st = set()

    while True:
        name = file.readline()
        address = file.readline()
        if not name:
            break
        st.add((name.strip(), address.strip()))

    file.close()

    ls = []
    for item in st:
        ls.append({"name": item[0], "address": item[1]})

    file_w = open("north_spine_locations.json", "w")
    json.dump(ls, file_w, indent=4)


if __name__ == "__main__":
    main()
