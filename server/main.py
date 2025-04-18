from flask import Flask
from blueprints.stairs import stairs_bp
from blueprints.toilet import toilet_bp
from blueprints.lifts import lift_bp

app = Flask(__name__)
app.config['MAX_CONTENT_LENGTH'] = 32 * 1024 * 1024

app.register_blueprint(stairs_bp, url_prefix='/api')
app.register_blueprint(toilet_bp, url_prefix='/api')
app.register_blueprint(lift_bp, url_prefix='/api')
