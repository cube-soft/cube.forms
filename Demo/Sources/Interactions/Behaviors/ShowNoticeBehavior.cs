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
using Cube.Forms.Behaviors;
using Cube.Forms.Controls;

namespace Cube.Forms.Demo
{
    /* --------------------------------------------------------------------- */
    ///
    /// ShowNoticeBehavior
    ///
    /// <summary>
    /// Represents the behavior to show a notice dialog.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public sealed class ShowNoticeBehavior : MessageBehavior<NoticeMessage>
    {
        #region Constructors

        /* --------------------------------------------------------------------- */
        ///
        /// ShowNoticeBehavior
        ///
        /// <summary>
        /// Initializes a new instance of the ShowNoticeBehavior class
        /// with the specified arguments.
        /// </summary>
        ///
        /// <param name="vm">ViewModel object.</param>
        ///
        /* --------------------------------------------------------------------- */
        public ShowNoticeBehavior(IPresentable vm) : base(vm) { }

        #endregion

        #region Implementations

        /* --------------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Invokes the action.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected override void Invoke(NoticeMessage m)
        {
            var view = new NoticeWindow();
            view.Selected += (s, e) => view.Close();
            view.SetTopMost(false);
            view.Set(m.Text, m.Title);
            view.Set(m.Style);
            view.Set(NoticeLocation.BottomRight);
            view.Show();
        }

        #endregion
    }
}
