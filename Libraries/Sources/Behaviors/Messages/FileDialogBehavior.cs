﻿/* ------------------------------------------------------------------------- */
//
// Copyright (c) 2010 CubeSoft, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
/* ------------------------------------------------------------------------- */
using System.Linq;
using System.Windows.Forms;
using Cube.Mixin.String;

namespace Cube.Forms.Behaviors
{
    #region OpenFileBehavior

    /* --------------------------------------------------------------------- */
    ///
    /// OpenFileBehavior
    ///
    /// <summary>
    /// Provides functionality to show a open-file dialog.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class OpenFileBehavior : MessageBehavior<OpenFileMessage>
    {
        /* ----------------------------------------------------------------- */
        ///
        /// OpenFileBehavior
        ///
        /// <summary>
        /// Initializes a new instance of the OpenFileBehavior class
        /// with the specified presentable object.
        /// </summary>
        ///
        /// <param name="vm">Presentable object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public OpenFileBehavior(IPresentable vm) : base (vm, e =>
        {
            var dialog = new OpenFileDialog
            {
                CheckPathExists = e.CheckPathExists,
                Multiselect     = e.Multiselect,
                FilterIndex     = e.FilterIndex,
            };

            if (e.Text.HasValue()) dialog.Title = e.Text;
            if (e.Value.Any()) dialog.FileName = e.Value.First();
            if (e.Filter.HasValue()) dialog.Filter = e.Filter;
            if (e.InitialDirectory.HasValue()) dialog.InitialDirectory = e.InitialDirectory;

            var ok = dialog.ShowDialog() == DialogResult.OK;
            e.Cancel = !ok;
            if (ok) e.Value = dialog.FileNames;
        }) { }
    }

    #endregion

    #region SaveFileBehavior

    /* --------------------------------------------------------------------- */
    ///
    /// SaveFileBehavior
    ///
    /// <summary>
    /// Provides functionality to show a save-file dialog.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class SaveFileBehavior : MessageBehavior<SaveFileMessage>
    {
        /* ----------------------------------------------------------------- */
        ///
        /// SaveFileBehavior
        ///
        /// <summary>
        /// Initializes a new instance of the SaveFileBehavior class
        /// with the specified presentable object.
        /// </summary>
        ///
        /// <param name="vm">Presentable object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public SaveFileBehavior(IPresentable vm) : base(vm, e =>
        {
            var dialog = new SaveFileDialog
            {
                CheckPathExists = e.CheckPathExists,
                OverwritePrompt = e.OverwritePrompt,
                FilterIndex     = e.FilterIndex,
            };

            if (e.Text.HasValue()) dialog.Title = e.Text;
            if (e.Value.HasValue()) dialog.FileName = e.Value;
            if (e.Filter.HasValue()) dialog.Filter = e.Filter;
            if (e.InitialDirectory.HasValue()) dialog.InitialDirectory = e.InitialDirectory;

            var ok = dialog.ShowDialog() == DialogResult.OK;
            e.Cancel = !ok;
            if (ok) e.Value = dialog.FileName;
        }) { }
    }

    #endregion

    #region OpenDirectoryBehavior

    /* --------------------------------------------------------------------- */
    ///
    /// OpenDirectoryBehavior
    ///
    /// <summary>
    /// Provides functionality to show a directory dialog.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class OpenDirectoryBehavior : MessageBehavior<OpenDirectoryMessage>
    {
        /* ----------------------------------------------------------------- */
        ///
        /// OpenDirectoryBehavior
        ///
        /// <summary>
        /// Initializes a new instance of the OpenDirectoryBehavior class
        /// with the specified presentable object.
        /// </summary>
        ///
        /// <param name="vm">Presentable object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public OpenDirectoryBehavior(IPresentable vm) : base(vm, e =>
        {
            var dialog = new FolderBrowserDialog { ShowNewFolderButton = e.NewButton };

            if (e.Text.HasValue()) dialog.Description = e.Text;
            if (e.Value.HasValue()) dialog.SelectedPath = e.Value;

            var ok = dialog.ShowDialog() == DialogResult.OK;
            e.Cancel = !ok;
            if (ok) e.Value = dialog.SelectedPath;
        }) { }
    }

    #endregion
}
