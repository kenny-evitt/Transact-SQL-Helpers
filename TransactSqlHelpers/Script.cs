namespace TransactSqlHelpers
{
    using System.Collections.Generic;
    using System.IO;

    public class Script
    {
        // Private fields

        private IEnumerable<Batch> _batches;
        private IEnumerable<string> _scriptLines;
        private readonly string _scriptPath;


        // Public property

        public IEnumerable<Batch> Batches
        {
            get
            {
                return _batches;
            }
        }


        // Constructor

        public Script(string scriptPath)
        {
            _scriptPath = scriptPath;
            _scriptLines = File.ReadAllLines(scriptPath);
            _batches = Scripts.GetBatches(_scriptLines);
        }
    }
}
