using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCopyUSB.Extensions
{
    public static class DirectoryInfoExtension
    {
        // Copies all files from one directory to another.
        public static void CopyTo(this DirectoryInfo source,
                string destDirectory, bool recursive, bool skipErrors = false, Func<bool> actionPerCoopy = null)
        {
            if (source == null || destDirectory == null || !source.Exists) return;

            DirectoryInfo target = null;
            try
            {
                // Target Directory
                target = new DirectoryInfo(destDirectory);
            }
            catch (Exception ex)
            {
                if (skipErrors)
                {
                    Utils.Log("DirectoryInfoExtension CopyTo - Can't initialize target object - " + ex.Message);
                    return;
                }
                else throw ex;
            }

            // If the target doesn't exist, we create it.
            try
            {
                if (!target.Exists) target.Create();
            }
            catch (Exception ex)
            {
                if (skipErrors)
                {
                    Utils.Log("DirectoryInfoExtension CopyTo - Can't create target - " + ex.Message);
                    return;
                }
                else throw ex;
            }

            try
            {
                // Get all files and copy them over.
                foreach (FileInfo file in source.GetFiles())
                {
                    try
                    {
                        file.CopyTo(Path.Combine(target.FullName, file.Name), true);
                    }
                    catch (Exception ex)
                    {
                        if (skipErrors)
                        {
                            Utils.Log($"DirectoryInfoExtension CopyTo - Can't copy file `{file.Name}` - {ex.Message}");
                            continue;
                        }
                        else throw ex;
                    }
                    if (actionPerCoopy != null && actionPerCoopy()) return;
                }
            } catch (Exception ex)
            {
                if (skipErrors) return; else throw ex;
            }

            try
            {
                // Return if no recursive call is required.
                if (!recursive) return;
                // Do the same for all sub directories.
                foreach (DirectoryInfo directory in source.GetDirectories())
                {
                    try
                    {
                        if (String.IsNullOrEmpty(target.FullName) || String.IsNullOrEmpty(directory.Name)) continue;
                        CopyTo(directory,
                        Path.Combine(target.FullName, directory.Name), recursive);
                        if (actionPerCoopy != null && actionPerCoopy()) return;
                    } catch (Exception ex)
                    {
                        if (!skipErrors) throw ex;
                    }
                }
            } catch (Exception ex)
            {
                if (!skipErrors) throw ex;
            }
        }

    }
}