// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace System.CommandLine
{
    public class TestConsole : IConsole
    {
        private readonly StandardStreamReader _reader;
        
        public TestConsole()
        {
            Out = new StandardStreamWriter();
            Error = new StandardStreamWriter();
            In = _reader = new StandardStreamReader();
        }

        public IStandardStreamWriter Error { get; protected set; }

        public IStandardStreamWriter Out { get; protected set; }
        
        public IStandardStreamReader In { get; protected set; }

        public bool IsOutputRedirected { get; protected set; }

        public bool IsErrorRedirected { get; protected set; }

        public bool IsInputRedirected { get; protected set; }

        public void AddLine(string line) => _reader.AddLine(line);

        internal class StandardStreamWriter : TextWriter, IStandardStreamWriter
        {
            private readonly StringBuilder _stringBuilder = new StringBuilder();

            public override void Write(char value)
            {
                _stringBuilder.Append(value);
            }

            public override Encoding Encoding { get; } = Encoding.Unicode;

            public override string ToString() => _stringBuilder.ToString();            
        }

        internal class StandardStreamReader : Queue<string>, IStandardStreamReader
        {            
            public void AddLine(string line)
            {
                Enqueue(line);
            }
            
            public string ReadLine() => Count > 0 
                ? Dequeue() + Environment.NewLine 
                : Environment.NewLine;

            public string ReadToEnd()
            {
                var lines = string.Join(Environment.NewLine, this.Select(x => x));
                Clear();
                lines += Environment.NewLine;

                return lines;
            }
        }
    }
}
