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
using System;
using System.Collections.Generic;
using WinForms = System.Windows.Forms;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// WindowBase
    ///
    /// <summary>
    /// Represents the base class of WinForms based window.
    /// </summary>
    ///
    /// <remarks>
    /// ViewModel や各種 Behaviors のための処理を実装します。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class WindowBase : WinForms.Form
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Presenter
        ///
        /// <summary>
        /// Gets or sets the presenter.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected IPresentable Presenter { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Behaviors
        ///
        /// <summary>
        /// Gets the collection of registered behaviors.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected IList<IDisposable> Behaviors { get; } = new List<IDisposable>();

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Bind
        ///
        /// <summary>
        /// Binds the window with the specified object.
        /// </summary>
        ///
        /// <param name="src">Object to be bound.</param>
        ///
        /* ----------------------------------------------------------------- */
        public virtual void Bind(IPresentable src) => Presenter = src;

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// Releases the unmanaged resources used by the StandardForm
        /// and optionally releases the managed resources.
        /// </summary>
        ///
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources;
        /// false to release only unmanaged resources.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (_disposed) return;
                _disposed = true;
                if (!disposing) return;

                foreach (var behavior in Behaviors) behavior.Dispose();
                Behaviors.Clear();
                Presenter?.Dispose();
                Presenter = null;
            }
            finally { base.Dispose(disposing); }
        }

        #endregion

        #region Fields
        private bool _disposed = false;
        #endregion
    }
}
