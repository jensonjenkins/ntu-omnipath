from waitress import serve
import main

if __name__ == "__main__":
    serve(main.app, host="0.0.0.0", port=8080)
