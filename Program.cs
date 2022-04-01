using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Text.Json;
using CommandLine;
using Seek4Treasure.Class;

namespace Seek4Treasure
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Goals
            /*
                * Take folder and control it IF it's valid continue 
                * List all file from folder with resursive
                * Take language name control IF t's valid just read valid extension
                * Extract file by extension IF t's valid just read valid extension
                * 
             */
            #endregion

            Core c = new Core();
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o =>
                { 
                    if (o.lang == "all")
                    {
                        var allfiles = c.listFileFromFolder(o.folder);
                        var extesions = c.returnExtensionListByLanguage(o.lang);
                        var files = c.extractFilesByExtension(allfiles, extesions);
                        List<outputModel> resultObject = c.result(files);

                        if (o.output == "false")
                        {
                            c.parseJson(resultObject);  
                        }
                        else
                        {
                            c.parseJson(resultObject,o.output);
                        }
                    }
                    else if (o.lang != "all")
                    {
                        var stat = c.languageControl(o.lang);
                        if (stat is true)
                        {
                            var allfiles = c.listFileFromFolder(o.folder);
                            var extesions = c.returnExtensionListByLanguage(o.lang);
                            var files = c.extractFilesByExtension(allfiles, extesions);
                            List<outputModel> resultObject = c.result(files);

                            if (o.output == "false")
                            {
                                c.parseJson(resultObject);
                            }
                            else
                            {
                                c.parseJson(resultObject, o.output);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Girdiğiniz dil için bir seçenek bulunmamaktadır.");
                            Environment.Exit(0);
                        }
                       
                    }
                });
        }
    }

}
