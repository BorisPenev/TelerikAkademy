var SimulateVehicles = (function () {

    Function.prototype.inherit = function(parent) {
        this.prototype = new parent();
        this.prototype.constructor = parent;
    };
    
    ////////////////////////////////////////////////////////////////////////////////
    // SimulateVehicles.Vehicle
    function Vehicle(speed, propulsionUnits) {
        this.speed = speed;
        this.propulsionUnits = propulsionUnits;
    };

    Vehicle.prototype.accelerate = function() {
        for (var i = 0; i < this.propulsionUnits.length; i++) {
            this.speed += this.propulsionUnits[i].getAcceleration();
        }
    };

    ////////////////////////////////////////////////////////////////////////////////
    // SimulateVehicles.PropulsionUnit
    function PropulsionUnit() {
    };

    ////////////////////////////////////////////////////////////////////////////////
    // SimulateVehicles.Wheel
    function Wheel(radius) {
        this.radius = 0;
        PropulsionUnit.call(this);
        this.radius = radius;
    };

    Wheel.inherit(PropulsionUnit);
    
    Wheel.prototype.getAcceleration = function() {
        return (2 * Math.PI * this.radius);
    };

    ////////////////////////////////////////////////////////////////////////////////
    // SimulateVehicles.PropellingNozzles
    function PropellingNozzle(power, afterburnerSwitch) {
        this.power = 0;
        this.afterburnerSwitch = false;
        PropulsionUnit.call(this);
        this.power = power;
        this.afterburnerSwitch = afterburnerSwitch;
    };

    PropellingNozzle.inherit(PropulsionUnit);
    
    PropellingNozzle.prototype.getAcceleration = function() {
        if (this.afterburnerSwitch) {
            return (this.power * 2);
        }
        return this.power;
    };
    
    ////////////////////////////////////////////////////////////////////////////////
    // SimulateVehicles.Propellers
    function Propeller(numberOfFins, spinDirection) {
        this.numberOfFins = 0;
        this.spinDirection = spinDirection.clockwise;
        PropulsionUnit.call(this);
        this.numberOfFins = numberOfFins;
        this.spinDirection = spinDirection;
    };
    
    Propeller.inherit(PropulsionUnit);
    
    Propeller.prototype.getAcceleration = function() {
        if (this.spinDirection === spinDirectionTypes.counterClockwise) {
            return -this.numberOfFins;
        }
        return this.numberOfFins;
    };

    ////////////////////////////////////////////////////////////////////////////////
    // SimulateVehicles.SpinDirections
    var spinDirectionTypes = {
        "clockwise": "clockwise",
        "counterClockwise": "counterClockwise"
    };
    
    ////////////////////////////////////////////////////////////////////////////////
    // SimulateVehicles.LandVehicle
    function LandVehicle(speed, wheelRadius) {
        var propulsionUnits = [];
        propulsionUnits.push(new Wheel(wheelRadius));
        propulsionUnits.push(new Wheel(wheelRadius));
        propulsionUnits.push(new Wheel(wheelRadius));
        propulsionUnits.push(new Wheel(wheelRadius));
        Vehicle.call(this, speed, propulsionUnits);
    };

    LandVehicle.inherit(Vehicle);
    
	////////////////////////////////////////////////////////////////////////////////
	// SimulateVehicles.AirVehicle
    function AirVehicle(speed) {
        var propulsionUnits = [];
        propulsionUnits.push(new PropellingNozzle(speed, false));
		Vehicle.call(this, speed, propulsionUnits);
    };

    AirVehicle.inherit(Vehicle);
    
    AirVehicle.prototype.switchAfterburners = function() {
        for (var i = 0; i < this.propulsionUnits.length; i++) {
            if (this.propulsionUnits[i] instanceof PropellingNozzle) {
                this.propulsionUnits[i].afterburnerSwitch = !this.propulsionUnits[i].afterburnerSwitch;
            }
        }
    };

    ////////////////////////////////////////////////////////////////////////////////
    // SimulateVehicles.WaterVehicle
    function WaterVehicle(speed, propulsionUnits) {
	    Vehicle.call(this, speed, propulsionUnits);
    };
    
    WaterVehicle.inherit(Vehicle);

    WaterVehicle.prototype.changeSpinDirection = function() {
        for (var i = 0; i < this.propulsionUnits.length; i++) {
            if (this.propulsionUnits[i] instanceof Propeller) {
                this.propulsionUnits[i].afterburnerSwitch = !this.propulsionUnits[i].afterburnerSwitch;
                switch (this.propulsionUnits[i].spinDirection) {
                    case spinDirectionTypes.clockwise:
                    {
                        this.propulsionUnits[i].spinDirection = spinDirectionTypes.counterClockwise;
                        break;
                    }
                default:
                    {
                        this.propulsionUnits[i].spinDirection = spinDirectionTypes.clockwise;
                        break;
                    }
                }
            }
        }
    };

	////////////////////////////////////////////////////////////////////////////////
	// SimulateVehicles.AmphibiousVehicle
    function AmphibiousVehicle(speed, propulsionUnits, isLand) {
        this.isLand = !isLand;
        this.speed = speed;
        this.propulsionUnits = propulsionUnits;
        this.switchMode();
    };
    
    AmphibiousVehicle.inherit(Vehicle);
    
    AmphibiousVehicle.prototype.switchMode = function() {
        this.isLand = !this.isLand;
        if (this.isLand) {
            return new LandVehicle(this.speed, 20);
        } else {
            return new WaterVehicle(this.speed, this.propulsionUnits);
        }
    };

    return {
        LandVehicle: LandVehicle,
        AirVehicle: AirVehicle,
        WaterVehicle: WaterVehicle,
        AmphibiousVehicle: AmphibiousVehicle,
        Wheel: Wheel,
        PropellingNozzle: PropellingNozzle,
        Propeller: Propeller,
        Vehicle: Vehicle,
        PropulsionUnit: PropulsionUnit,
        spinDirectionTypes: spinDirectionTypes
    };	
	
})();
