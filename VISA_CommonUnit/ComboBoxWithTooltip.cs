using System;
using System.Drawing;
using System.Windows.Forms;

/*
 * ==============================================================
 * @ID       $Id: ComboBoxWithTooltip.cs 1228 2012-08-05 19:47:44Z ww $
 * @created  2011-02-20
 * @project  http://cleancode.sourceforge.net/
 * ==============================================================
 *
 * The official license for this file is shown next.
 * Unofficially, consider this e-postcardware as well:
 * if you find this module useful, let us know via e-mail, along with
 * where in the world you are and (if applicable) your website address.
 */

/* ***** BEGIN LICENSE BLOCK *****
 * Version: MPL 1.1
 *
 * The contents of this file are subject to the Mozilla Public License Version
 * 1.1 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS" basis,
 * WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
 * for the specific language governing rights and limitations under the
 * License.
 *
 * The Original Code is part of the CleanCode toolbox.
 *
 * The Initial Developer of the Original Code is Michael Sorens.
 * Portions created by the Initial Developer are Copyright (C) 2011
 * the Initial Developer. All Rights Reserved.
 *
 * Contributor(s):
 *
 * ***** END LICENSE BLOCK *****
 */

namespace CleanCode.GeneralComponents.Controls
{
    /// <summary>
    /// Represents a Windows combo box control with each item in the dropdown rendering a tooltip.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A <see cref="ComboBox"/> displays a text box combined with a ListBox, 
    /// which enables the user to select items from the list or enter a new value.
    /// This modified ComboBox provides support for each item in the
    /// drop down list to display a tooltip with the same text as the item itself.
    /// This is useful for a <c>ComboBox</c> that is particularly narrow, or for contents
    /// of its list that are particularly wide (e.g. file paths).
    /// </para>
    /// <para>
    /// This class exposes the same properties, methods, and events as the standard <c>ComboBox</c>.
    /// Simply instantiating this class provides automatic support for tooltips;
    /// there is nothing to set.
    /// </para>
    /// <para>
    /// This code comes from my StackOverflow answer to "Tooltip for each items in a combo box"
    /// at-http://stackoverflow.com/questions/680373/tooltip-for-each-items-in-a-combo-box/5053730#5053730,
    /// which is a refinement to the answer from Zhi-Xin Ye on the MSDN post "Windows Dropdown question"
    /// at-http://social.msdn.microsoft.com/forums/en-US/winforms/thread/e0418c61-a614-4f0d-8cf4-59cf9cc5bba5/.
    /// </para>
    /// <para>
    /// Since CleanCode 1.1.01.
    /// </para>
    /// </remarks>
    public class ComboBoxWithTooltip : ComboBox
    {
        private readonly ToolTip toolTip1 = new ToolTip();

        /// <summary>
        /// Initializes a new instance of the <see cref="ComboBoxWithTooltip"/> class.
        /// </summary>
        public ComboBoxWithTooltip()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.ComboBox.DrawItem"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DrawItemEventArgs"/> that contains the event data.</param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            // Needed gate when DropDownStyle set to DropDownList (thanks to "Andrew" remarking on my 
            // StackOverflow post (stackoverflow.com/questions/680373/tooltip-for-each-items-in-a-combo-box/).
            if (e.Index < 0) { return; }

            string text = GetItemText(Items[e.Index]);
            e.DrawBackground();
            using (SolidBrush br = new SolidBrush(e.ForeColor))
            { e.Graphics.DrawString(text, e.Font, br, e.Bounds); }
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            { toolTip1.Show(text, this, e.Bounds.Right, e.Bounds.Bottom); }
            e.DrawFocusRectangle();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.ComboBox.DropDownClosed"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnDropDownClosed(EventArgs e)
        {
            toolTip1.Hide(this);
            base.OnDropDownClosed(e);
        }
    }
}
