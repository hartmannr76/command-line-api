// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;

namespace System.CommandLine
{
    public static class StandardStreamReader
    {
        public static IStandardStreamReader Create(TextReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            return new AnonymousStandardStreamReader(reader.ReadLine, reader.ReadToEnd);
        }

        public static string ReadLine(this IStandardStreamReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            return reader.ReadLine();
        }
        
        public static string ReadToEnd(this IStandardStreamReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            return reader.ReadToEnd();
        }

        private class AnonymousStandardStreamReader : IStandardStreamReader
        {
            private readonly Func<string> _readLine;
            private readonly Func<string> _readToEnd;

            public AnonymousStandardStreamReader(Func<string> readLine, Func<string> readToEnd)
            {
                _readLine = readLine;
                _readToEnd = readToEnd;
            }

            public string ReadLine() => _readLine();
            public string ReadToEnd() => _readToEnd();
        }
    }
}
