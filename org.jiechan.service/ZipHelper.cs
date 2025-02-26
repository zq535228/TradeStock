﻿using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.Net;

namespace org.jiechan.service {
    public class ZipHelper {

        /// <summary>
        /// Create a zip archive.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="directory">The directory to zip.</param> 
        public static void Zip(string filename, string directory) {
            try {
                _createDirtory(filename);
                FastZip fz = new FastZip();
                fz.CreateEmptyDirectories = true;
                fz.CreateZip(filename, directory, true, "");
                fz = null;
            } catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// Unpacks the files.
        /// 
        ///     string filePath = PathHelper.TempPath + "\\忍者x3-ZBlog1.8.zip";
        ///     if (!File.Exists(filePath)) {
        ///        string fileUrl = "http://update.renzhe.org/tools/忍者x3-ZBlog1.8.zip";
        ///        new RzHttp().DownLoadFile(fileUrl, "忍者x3-ZBlog1.8.zip");
        ///     }
        ///
        /// ZipHelper.UnZip(filePath, PathHelper.TempPath + "\\忍者x3-ZBlog1.8\\");

        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>if succeed return true,otherwise false.</returns>
        public static bool UnZip(string file, string dir) {
            return UnZip(file, dir, false);
        }

        public static bool UnZip(string file, string dir, bool showinfo) {
            try {
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                if (!dir.EndsWith("\\")) {
                    dir += "\\";
                }
                if (showinfo)
                    EchoHelper.Echo("文件解压缩开始，请耐心等待...", "解压缩", EchoHelper.EchoType.绿色信息);

                ZipInputStream s = new ZipInputStream(File.OpenRead(file));

                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null) {

                    string directoryName = Path.GetDirectoryName(theEntry.Name);
                    string fileName = Path.GetFileName(theEntry.Name);

                    if (directoryName != String.Empty)
                        Directory.CreateDirectory(dir + directoryName);

                    if (fileName != String.Empty) {
                        FileStream streamWriter = File.Create(dir + theEntry.Name);

                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true) {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0) {
                                streamWriter.Write(data, 0, size);
                            } else {
                                break;
                            }
                        }

                        streamWriter.Close();
                    }
                    if (showinfo)
                        EchoHelper.Echo("解压缩：" + theEntry.Name, "解压缩", EchoHelper.EchoType.绿色信息);


                }
                s.Close();

                if (showinfo)
                    EchoHelper.Echo("成功解压缩到：" + dir, "解压缩", EchoHelper.EchoType.绿色信息);

                return true;
            } catch (Exception ex) {
                EchoHelper.EchoException(ex);
                return false;
            }
        }



        //创建目录
        private static void _createDirtory(string path) {
            if (!File.Exists(path)) {
                string[] dirArray = path.Split('\\');
                string temp = string.Empty;
                for (int i = 0; i < dirArray.Length - 1; i++) {
                    temp += dirArray[i].Trim() + "\\";
                    if (!Directory.Exists(temp))
                        Directory.CreateDirectory(temp);
                }
            }
        }

    }
}
