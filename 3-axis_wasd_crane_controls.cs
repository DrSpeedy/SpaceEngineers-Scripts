// Control Script for my (currently unpublished) 3d printer blueprint
// WASD - X and Y axis movement
// C & [Space] - Z axis movement
// The game uses a XZY scheme for coordinates, but this script will reference them all as
// standard XYZ coordinates.
// Must have a cockpit named "Industrial Cockpit", it does not matter if it is that one or another,
// the name just needs to be that. The script will use the movement data for the cockpit to move the pistons
// along their axis

IMyCockpit _controller;
IMyBlockGroup _x_axis_1;
IMyBlockGroup _x_axis_2;
IMyBlockGroup _y_axis_1;
IMyBlockGroup _y_axis_2;
IMyBlockGroup _z_axis;

public Program() {
	_controller = (IMyCockpit) GridTerminalSystem.GetBlockWithName("Industrial Cockpit");
	_x_axis_1 = GridTerminalSystem.GetBlockGroupWithName("Pistons X-Axis 1");
	_x_axis_2 = GridTerminalSystem.GetBlockGroupWithName("Pistons X-Axis 2");
	_y_axis_1 = GridTerminalSystem.GetBlockGroupWithName("Pistons Y-Axis 1");
	_y_axis_2 = GridTerminalSystem.GetBlockGroupWithName("Pistons Y-Axis 2");
	_z_axis = GridTerminalSystem.GetBlockGroupWithName("Pistons Z-Axis");
	
	// Set the continuous update frequency of this script
    Runtime.UpdateFrequency = UpdateFrequency.Update1;
}

void Main(string args) {
	var movement_data = _controller.MoveIndicator;
	var speed_const = 0.3f;
	
	if (movement_data.X == 1) { // right
		SetPistonSpeed(_x_axis_1, speed_const);
		SetPistonSpeed(_x_axis_2, -1.0f * speed_const);
	}
	else if (movement_data.X == -1) { // left
		SetPistonSpeed(_x_axis_1, -1.0f * speed_const);
		SetPistonSpeed(_x_axis_2, speed_const);
	}
	else if (movement_data.X == 0) {
		SetPistonSpeed(_x_axis_1, 0.0f);
		SetPistonSpeed(_x_axis_2, 0.0f);
	}
	
	if (movement_data.Z == -1) { // forward
		SetPistonSpeed(_y_axis_1, speed_const);
		SetPistonSpeed(_y_axis_2, -1.0f * speed_const);
	}
	else if (movement_data.Z == 1) { // back
		SetPistonSpeed(_y_axis_1, -1.0f * speed_const);
		SetPistonSpeed(_y_axis_2, speed_const);
	}
	else if (movement_data.Z == 0) {
		SetPistonSpeed(_y_axis_1, 0.0f);
		SetPistonSpeed(_y_axis_2, 0.0f);
	}
	
	if (movement_data.Y == -1) { // down
		SetPistonSpeed(_z_axis, speed_const);
	}
	else if (movement_data.Y == 1) { // up
		SetPistonSpeed(_z_axis, -1.0f * speed_const);
	}
	else if (movement_data.Y == 0) {
		SetPistonSpeed(_z_axis, 0.0f);
	}
	
}

void SetPistonSpeed(IMyBlockGroup pistons, float speed) {
	var blocks = new List<IMyTerminalBlock>();
	pistons.GetBlocks(blocks);
	foreach (var block in blocks) {
		((IMyPistonBase) block).Velocity = speed;
	}
}
