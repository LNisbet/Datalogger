

using SQLight_Database.Config.Interface;
using SQLight_Database.Models;

namespace SQLight_Database.Config
{
    public class ExerciseTableConfig : ITableConfig<Exercise>
    {
        public string Name => "Values";

        public List<ColumnDescription> Description  =>
        [
            new ColumnDescription("Name", "VARCHAR(32)", "PRIMARY KEY"),
            new ColumnDescription("Tags", "TEXT"),
            new ColumnDescription("Unit1", "VARCHAR(20)", "NOT NULL"),
            new ColumnDescription("Unit2", "VARCHAR(20)"),
            new ColumnDescription("Unit3", "VARCHAR(20)"),
            new ColumnDescription("Unit4", "VARCHAR(20)"),
            new ColumnDescription("Description", "TEXT")
        ];

        public List<Exercise>? DefaultValues =>
        [
            new Exercise("Weight", [], Exercise.Units.Kg),
            new Exercise("Left Little Finger Open Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Left hand little finger in a drag position"),
            new Exercise("Left Little Finger Half Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Left hand little finger in a Half crimp position"),
            new Exercise("Left Ring Finger Open Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Left hand ring finger in a drag position"),
            new Exercise("Left Ring Finger Half Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Left hand ring finger in a Half crimp position"),
            new Exercise("Left Middle Finger Open Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Left hand middle finger in a drag position"),
            new Exercise("Left Middle Finger Half Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Left hand middle finger in a Half crimp position"),
            new Exercise("Left Index Finger Open Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Left hand index finger in a drag position"),
            new Exercise("Left Index Finger Half Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Left hand index finger in a half crimp position"),
            new Exercise("Right Little Finger Open Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Right hand little finger in a drag position"),
            new Exercise("Right Little Finger Half Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Right hand little finger in a half crimp position"),
            new Exercise("Right Ring Finger Open Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Right hand ring finger in a drag position"),
            new Exercise("Right Ring Finger Half Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Right hand ring finger in a half crimp position"),
            new Exercise("Right Middle Finger Open Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Right hand middle finger in a drag position"),
            new Exercise("Right Middle Finger Half Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Right hand middle finger in a half crimp position"),
            new Exercise("Right Index Finger Open Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Right hand index finger in a drag position"),
            new Exercise("Right Index Finger Half Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Right hand index finger in a half crimp position"),
            new Exercise("Left Half Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Left hand in a half crimp position"),
            new Exercise("Left Open Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Left hand in a drag position"),
            new Exercise("Left Full Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Left hand in a full crimp position"),
            new Exercise("Right Half Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Right hand in a half crimp position"),
            new Exercise("Right Open Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Right hand in a drag position"),
            new Exercise("Right Full Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Right hand in a full crimp position"),
            new Exercise("Pullup", ["UpperBody", "Strength"], Exercise.Units.Kg,Exercise.Units.Reps,null,null,"Full ROM pullup")
        ];
    }
}
