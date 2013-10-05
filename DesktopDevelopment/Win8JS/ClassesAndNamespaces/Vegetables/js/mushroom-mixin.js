var MushroomMixin = {
    grow: function (water) {
        if (this.radius) {
            this.radius = this.radius * water;
        }
        if (this.length) {
            this.length = this.length * water;
        }
    }
}