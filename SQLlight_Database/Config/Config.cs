using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SQLight_Database
{
    static internal class Config
    {
        #region Users File
        internal const string UsersPathName = "users.json";
        #endregion

        #region Tag Table
        internal const string TagsTableName = "Tags";

        static internal List<ColumnDescription> TagsTableDescription =
        [
            new ColumnDescription("Tags", "TEXT", "PRIMARY KEY")
        ];

        static internal readonly List<string> StandardTags = 
        [
            "Testing",
            "UpperBody", 
            "LowerBody",
            "Core",
            "Fingers",
            "Endurance",
            "Strength",
            "Power",
            "Rehab",
            "Flexibility"
        ];
        #endregion

        #region Exercise Table
        internal const string ExercieseTableName = "Exercises";

        static internal List<ColumnDescription> ExerciseTableDescription =
        [
            new ColumnDescription("Name", "VARCHAR(20)", "PRIMARY KEY"),
            new ColumnDescription("Tags", "TEXT", "NOT NULL"),
            new ColumnDescription("Unit1", "VARCHAR(20)", "NOT NULL"),
            new ColumnDescription("Unit2", "VARCHAR(20)"),
            new ColumnDescription("Unit3", "VARCHAR(20)"),
            new ColumnDescription("Unit4", "VARCHAR(20)"),
            new ColumnDescription("Description", "TEXT")
        ];

        static internal readonly List<Exercise> StandardExercises =
        [
            new Exercise("Weight", ["Testing"], Enums.Units.Kg),
            new Exercise("Max Left Little Open", ["Testing","Fingers"], Enums.Units.Kg,null,null,null,"Left hand little finger in a drag position"),
            new Exercise("Max Left Little Half", ["Testing","Fingers"], Enums.Units.Kg,null,null,null,"Left hand little finger in a half crimp position"),
            new Exercise("Max Left Ring Open", ["Testing","Fingers"], Enums.Units.Kg,null,null,null,"Left hand ring finger in a drag position"),
            new Exercise("Max Left Ring Half", ["Testing","Fingers"], Enums.Units.Kg,null,null,null,"Left hand ring finger in a half crimp position"),
            new Exercise("Max Left Middle Open", ["Testing","Fingers"], Enums.Units.Kg,null,null,null,"Left hand middle finger in a drag position"),
            new Exercise("Max Left Middle Half", ["Testing","Fingers"], Enums.Units.Kg,null,null,null,"Left hand middle finger in a half crimp position"),
            new Exercise("Max Left Index Open", ["Testing","Fingers"], Enums.Units.Kg,null,null,null,"Left hand index finger in a drag position"),
            new Exercise("Max Left Index Half", ["Testing","Fingers"], Enums.Units.Kg,null,null,null,"Left hand index finger in a half crimp position"),
            new Exercise("Max Right Little Open", ["Testing","Fingers"], Enums.Units.Kg,null,null,null,"Right hand little finger in a drag position"),
            new Exercise("Max Right Little Half", ["Testing","Fingers"], Enums.Units.Kg,null,null,null,"Right hand little finger in a half crimp position"),
            new Exercise("Max Right Ring Open", ["Testing","Fingers"], Enums.Units.Kg,null,null,null,"Right hand ring finger in a drag position"),
            new Exercise("Max Right Ring Half", ["Testing","Fingers"], Enums.Units.Kg,null,null,null,"Right hand ring finger in a half crimp position"),
            new Exercise("Max Right Middle Open", ["Testing","Fingers"], Enums.Units.Kg,null,null,null,"Right hand middle finger in a drag position"),
            new Exercise("Max Right Middle Half", ["Testing","Fingers"], Enums.Units.Kg,null,null,null,"Right hand middle finger in a half crimp position"),
            new Exercise("Max Right Index Open", ["Testing","Fingers"], Enums.Units.Kg,null,null,null,"Right hand index finger in a drag position"),
            new Exercise("Max Right Index Half", ["Testing","Fingers"], Enums.Units.Kg,null,null,null,"Right hand index finger in a half crimp position"),
            new Exercise("Max Left Half", ["Testing","Fingers"], Enums.Units.Kg,null,null,null,"Left hand in a half crimp position"),
            new Exercise("Max Left Open", ["Testing","Fingers"], Enums.Units.Kg,null,null,null,"Left hand in a drag position"),
            new Exercise("Max Left Full", ["Testing","Fingers"], Enums.Units.Kg,null,null,null,"Left hand in a full crimp position"),
            new Exercise("Max Right Half", ["Testing","Fingers"], Enums.Units.Kg,null,null,null,"Right hand in a half crimp position"),
            new Exercise("Max Right Open", ["Testing","Fingers"], Enums.Units.Kg,null,null,null,"Right hand in a drag position"),
            new Exercise("Max Right Full", ["Testing","Fingers"], Enums.Units.Kg,null,null,null,"Right hand in a full crimp position"),
            new Exercise("Max 20mm Half", ["Testing","Fingers"], Enums.Units.Kg,null,null,null,"Right hand in a full crimp position"),
            new Exercise("Max 15mm Half", ["Testing","Fingers"], Enums.Units.Kg,null,null,null,"Right hand in a full crimp position"),
            new Exercise("Max Pullup", ["Testing","UpperBody"], Enums.Units.Kg,Enums.Units.Reps,null,null,"Right hand in a full crimp position")
        ];
        #endregion

        #region Logs Table
        internal const string LogsTableName = "ExerciseLogs";

        static internal List<ColumnDescription> LogTableDescription =
        [
            new ColumnDescription("Id", "INTEGER PRIMARY KEY"),
            new ColumnDescription("Date", "DATE", "NOT NULL"),
            new ColumnDescription("Exercise", "VARCHAR(20)", "NOT NULL"),
            new ColumnDescription("Value1", "FLOAT", "NOT NULL"),
            new ColumnDescription("Value2", "FLOAT"),
            new ColumnDescription("Value3", "FLOAT"),
            new ColumnDescription("Value4", "FLOAT"),
            new ColumnDescription("Note", "TEXT")
        ];
        #endregion
    }
}
