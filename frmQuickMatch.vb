Imports ggcAppDriver
Imports System.Windows.Forms
Imports ADODB

Public Class frmQuickMatch
    
    Private Sub cmdButton00_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdButton00.Click
        System.Diagnostics.Process.Start("D:\GGC_Systems\vb.net\Lender\QMProcessor.exe")
    End Sub
End Class