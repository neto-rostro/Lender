Option Explicit On

Imports ggcAppDriver
Imports ggcLRTransaction

Public Class frmLRApplicationTransfer
    Private WithEvents poTrans As LRApplicationTransfer

    Private pnLoadx As Integer
    Private poControl As Control

    Private pnIndex As Integer
    Private pbCtrlPressed As Boolean
    Private pbGridFocused As Boolean

    Private Sub frmLRApplicationTransfer_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If pnLoadx = 1 Then
            pnLoadx = 2
        End If
    End Sub

    Private Sub frmLRApplicationTransfer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If pnLoadx = 0 Then
            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "LostFocus", AddressOf txtField_LostFocus)
            Call grpKeyHandler(Me, GetType(TextBox), "txtSeeks", "KeyDown", AddressOf txtField_KeyDown)
            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)

            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf txtField_KeyDown)

            initData()

            pnLoadx = 1
        End If
    End Sub

    Private Sub frmLRApplicationTransfer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.ControlKey Then
            pbCtrlPressed = True
        Else
            If pbCtrlPressed Then
                Select Case e.KeyCode
                    Case Keys.Up
                        If pnIndex > 0 Then
                            dgView.ClearSelection()
                            dgView.CurrentCell = dgView.Rows(pnIndex - 1).Cells(0)
                        End If
                    Case Keys.Down
                        If pnIndex < poTrans.ItemCount - 1 Then
                            dgView.ClearSelection()
                            dgView.CurrentCell = dgView.Rows(pnIndex + 1).Cells(0)
                        End If
                End Select
            Else
                If pbGridFocused Then
                    If e.KeyCode = Keys.Space Then
                        dgView.Rows(pnIndex).Cells(5).Value = IIf(dgView.Rows(pnIndex).Cells(5).Value = 1, 0, 1)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub frmLRApplicationTransfer_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.ControlKey Then
            pbCtrlPressed = False
        End If
    End Sub

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button
        loChk = CType(sender, System.Windows.Forms.Button)
        Dim lnIndex As Integer
        lnIndex = Val(Mid(loChk.Name, 10))

        Select Case lnIndex
            Case 0 'exit
                Me.Dispose()
            Case 3 'save
                Dim lnCtr As Integer

                lnIndex = 0
                For lnCtr = 0 To dgView.Rows.Count - 1
                    If dgView.Rows(pnIndex).Cells(5).Value = "1" Then
                        poTrans.Detail(lnCtr, "cUpdteRec") = "1"
                        lnIndex += 1
                    End If
                Next

                If lnIndex = 0 Then
                    MsgBox("No transaction is modified.", MsgBoxStyle.Exclamation, "Warning")
                    Exit Sub
                End If

                If poTrans.SaveRecord() Then
                    MsgBox("Updates save successfully.", MsgBoxStyle.Information, "Success")
                    initData()
                End If
        End Select
    End Sub

    Private Sub initData()
        poTrans = New LRApplicationTransfer
        poTrans.AppDriver = p_oAppDriver

        txtSeeks00.Text = ""
        txtSeeks01.Text = ""

        dgView.Rows.Clear()

        txtSeeks00.Focus()
    End Sub

    Private Sub txtField_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        loTxt.BackColor = Color.Azure

        poControl = loTxt

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))

        loTxt.SelectAll()
    End Sub

    Private Sub txtField_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        loTxt.BackColor = SystemColors.Window

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))
    End Sub

    Private Sub txtField_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Enter Then
            Dim loTxt As Control
            loTxt = CType(sender, System.Windows.Forms.TextBox)

            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))

            If Mid(loTxt.Name, 1, 8) = "txtSeeks" Then
                Select Case loIndex
                    Case 0
                        If poTrans.SearchRecord(loTxt.Text, False) Then
                            loadData()
                        End If
                    Case 1
                        poTrans.SearchBranch(loTxt.Text, False)
                End Select
            Else

            End If
        End If
    End Sub

    Private Sub loadData()
        Dim lnCtr As Integer

        With dgView
            .Rows.Clear()

            lnCtr = 0

            Do While lnCtr < poTrans.ItemCount
                .Rows.Add()
                .Rows(lnCtr).Cells(0).Value = poTrans.Detail(lnCtr, "sBranchNm")
                .Rows(lnCtr).Cells(1).Value = Format(CDate(poTrans.Detail(lnCtr, "dAppliedx")), "MMM dd, yyyy")
                .Rows(lnCtr).Cells(2).Value = poTrans.Detail(lnCtr, "sCompnyNm")
                .Rows(lnCtr).Cells(3).Value = poTrans.Detail(lnCtr, "sQMatchNo")
                .Rows(lnCtr).Cells(4).Value = poTrans.Detail(lnCtr, "sGOCASNox")

                lnCtr = lnCtr + 1
            Loop

            If .Rows.Count >= 9 Then
                .Columns(2).Width = 182
            Else
                .Columns(2).Width = 200
            End If
        End With
    End Sub

    Private Sub poTrans_MasterRetreive(ByVal Index As Integer, ByVal Value As Object) Handles poTrans.MasterRetreive
        Select Case Index
            Case 1
                txtSeeks01.Text = Value
        End Select
    End Sub

    Private Sub dgView_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgView.CellMouseClick
        pnIndex = e.RowIndex
    End Sub

    Private Sub dgView_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgView.GotFocus
        pbGridFocused = True
    End Sub

    Private Sub dgView_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgView.LostFocus
        pbGridFocused = False
    End Sub

    Private Sub dgView_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgView.RowEnter
        pnIndex = e.RowIndex
    End Sub
End Class