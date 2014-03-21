namespace TransactSqlHelpers
{
    using System.Collections.Generic;
    using System.IO;

    public class Script
    {
        // Private fields

        private IEnumerable<Batch> _batches;
        private readonly string _scriptPath;
        private readonly string _scriptText;


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
            _scriptText = File.ReadAllText(scriptPath);
            _batches = Scripts.GetBatches(_scriptText);
        }
    }
}
