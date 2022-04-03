using Seek4Treasure.Control;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Seek4Treasure.Class
{
    class Core
    {
        #region Variable
        
        // Extension list
        public List<KeyValuePair<string, List<string>>> languageExtensionList = new List<KeyValuePair<string, List<string>>>()
        {
            new KeyValuePair<string, List<string>>("csharp",new List<string>() {".cs",}),
            new KeyValuePair<string, List<string>>("python",new List<string>() {".py",}),
            new KeyValuePair<string, List<string>>("ruby",new List<string>() {".rb",}),
            new KeyValuePair<string, List<string>>("c",new List<string>() {".c",}),
            new KeyValuePair<string, List<string>>("js",new List<string>() {".js",".ts",".json"}),
            new KeyValuePair<string, List<string>>("php",new List<string>() {".php",}),
            new KeyValuePair<string, List<string>>("all",new List<string>() 
            {
                ".cs", 
                ".py", 
                ".rb", 
                ".c", 
                ".js",
                ".ts",
                ".json",
                ".php",
                //others
                ".txt",
                ".html",
                ".css",
                ".md",
            })
        };

        #endregion

        #region functions

        /// Description : control extension is in languageExtensionList
        /// Input       : scharp     / fortran
        /// Output      : true      / false
        public bool languageControl(string language)
        {
            var status = false;

            foreach (var item in languageExtensionList)
            {
                if (item.Key == language.ToLower()) { status = true; }
            }

            return status;
        }

        /// Description : return Extension By Language
        /// Input       : scharp     / fortran
        /// Output      : .sh      / null
        public List<string> returnExtensionListByLanguage(string language)
        {
            var extensions = new List<string>();

            foreach (var lang in languageExtensionList)
            {
                if (lang.Key == language.ToLower())
                {
                    foreach (var item in lang.Value)
                    {
                        extensions.Add(item);
                    }
                }
            }

            return extensions;
        }

        /// Description : Extract file by extension which user select language
        /// Input       : File List and Extension List(.sh, dll)
        /// Output      : asd.sh, asd.dll      / null
        public List<string> extractFilesByExtension(List<string> allfiles, List<string> extensions)
        {
            var files = new List<string>();

            foreach (var file in allfiles)
            {
                var ext = Path.GetExtension(file);

                foreach (var extension in extensions)
                {
                    if (extension == ext)
                    {
                        files.Add(file);
                    }
                }
            }

            return files;
        }

        /// Description : Folder Control
        /// Input       : Folder path
        /// Output      : true      / false
        public bool isValid(string path)
        {
            if (Directory.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// Description : List all files by Folder
        /// Input       : Folder path
        /// Output      : Files List      / null
        public List<string> listFileFromFolder(string path)
        {
            var control = isValid(path);

            if (control is true)
            {
                string[] fileEntries = Directory.GetFiles(path, "*", SearchOption.AllDirectories);

                var files = new List<string>();
                foreach (var item in fileEntries)
                {
                    files.Add(item);
                }
                return files;
            }
            else
            {
                return null;
            }
        }

        /// Description : fileRead and search Regex
        /// Input       : filename
        /// Output      : List<outputModel> (FileName, FileNumber, Data)     / null
        public List<outputModel> fileRead(string filename)
       {
            Password pass = new Password();
            Token token = new Token();
            Card card = new Card();

            var result = new List<outputModel>();
            var counter = 0;

            try
            {
                foreach (string line in File.ReadLines(filename))
                {
                    counter++;

                    foreach (var item in pass.Control(line))
                    {
                        result.Add(new outputModel() {
                            fileName = filename,
                            lineNumber = counter.ToString(),
                            line = item.Item1,
                            regexRule = item.Item2
                        });
                    }

                    foreach (var item in token.Control(line))
                    {
                        result.Add(new outputModel()
                        {
                            fileName = filename,
                            lineNumber = counter.ToString(),
                            line = item.Item1,
                            regexRule = item.Item2
                        });
                    }

                    foreach (var item in card.Control(line))
                    {
                        result.Add(new outputModel()
                        {
                            fileName = filename,
                            lineNumber = counter.ToString(),
                            line = item.Item1,
                            regexRule = item.Item2
                        });
                    }

                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return result;
        }

        /// Description : Take vulnerable object and make them one list for resut
        /// Input       : List<string> Files
        /// Output      : List<outputModel> result
        public List<outputModel> result(List<string> files)
        {
            List<outputModel> tmp = new List<outputModel>();

            foreach (var file in files)
            {
                foreach (var item in fileRead(file))
                {
                    tmp.Add(item);
                }
            }

            return tmp;
        }

        /// Description : Generate JSON Format to console
        /// Input       : List<outputModel> result
        /// Output      : write output to console
        public void parseJson(List<outputModel> result)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            Console.WriteLine(JsonSerializer.Serialize(result, serializeOptions));
        }
        
        /// Description : Generate JSON Format to file
        /// Input       : List<outputModel> result
        /// Output      : write output to file
        public void parseJson(List<outputModel> result, string folder)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            var data = JsonSerializer.Serialize(result, serializeOptions);
            try
            {
                File.WriteAllText(folder + "Seek4TreasureResult.json", data);

            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error while create Seek4TreasureResult.json file. Please control path which you giving or permission" + ex.ToString());
            }
        }

        /// Description : Take parametre from arguments and parse them
        /// Input       : login.php, profiel.js
        /// Output      : |List<stripng>| [0] => login.php , [1] = > profile.js 
        public List<string> excludeParamParser(string excludeParam)
        {
            var values = new List<string>();
            foreach (var item in excludeParam.Split(","))
            {
                values.Add(item);
            }

            return values;

        }

        /// Description : Extract file by extension which user give
        /// Input       : File List (test.php, test.cs, test.json) and Extension List(.json,.cs)
        /// Output      : test.php    
        public List<string> excludeFilesByExtension(List<string> allfiles, List<string> extensions)
        {
            var files = new List<string>();

            foreach (var file in allfiles)
            {
                var ext = Path.GetExtension(file);
                var status = false;

                foreach (var extension in extensions)
                {
                    if (extension == ext)
                    {
                        status = true;
                    }
                }

                if (status is false) { files.Add(file); }

            }

            return files;
        }

        /// Description : Extract file by file which user give
        /// Input       : File List (test.php, test.cs, test.json) and File List(test.php)
        /// Output      : test.cs, test.json
        public List<string> excludeFileByFileAndExtension(List<string> allfiles, List<string> fileNames)
        {
            var files = new List<string>();

            foreach (var file in allfiles)
            {
                var filename = Path.GetFileName(file);
                var status = false;

                foreach (var fileName in fileNames)
                {
                    if (filename == fileName)
                    {
                        status = true;
                    }
                }
                if (status is false) { files.Add(file); }
            }

            return files;
        }

        /// Description : Extract file by filename which user give
        /// Input       : File List (test.php, test.cs, test.json,deneme.php) and File List(test)
        /// Output      : deneme.php
        public List<string> excludeFileByName(List<string> allfiles, List<string> fileNames)
        {
            var files = new List<string>();

            foreach (var file in allfiles)
            {
                var filename = Path.GetFileNameWithoutExtension(file);
                var status = false;

                foreach (var fileName in fileNames)
                {
                    if (filename == fileName)
                    {
                        status = true;
                    }
                }
                if (status is false) { files.Add(file); }
            }

            return files;
        }


        #endregion
    }
}
