using ExcelDna.Integration.CustomUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TMTech.FeedersModel
{


    [ComVisible(true)]
    public class FeedersModelRibbon : ExcelRibbon
    {

        public FeedersModelRibbon()
        {

        }


        public override string GetCustomUI(string RibbonID)
        {
            return
@"
            <customUI xmlns = 'http://schemas.microsoft.com/office/2006/01/customui' >
                <ribbon>
                    <tabs>
                        <tab id = 'FeedersModelTab' label = 'FeedersModel' >
                            <group id = 'Group1' label = 'Execution' >
                                <button id = 'runButton' label = 'Run' onAction = 'OnButtonPressed' />
                            </group>
                        </tab>
                    </tabs>
               </ribbon>
            </customUI>
";

        }


        public void OnButtonPressed(IRibbonControl control)
        {
            MessageBox.Show("Hello from control " + control.Id);
        }


    }
}
