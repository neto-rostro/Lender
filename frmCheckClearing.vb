'€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€
' Guanzon Software Engineering Group
' Guanzon Group of Companies
' Perez Blvd., Dagupan City
'
'     Check Clearing Form
'
' Copyright 2016 and Beyond
' All Rights Reserved
' ºººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººº
' €  All  rights reserved. No part of this  software  €€  This Software is Owned by        €
' €  may be reproduced or transmitted in any form or  €€                                   €
' €  by   any   means,  electronic   or  mechanical,  €€    GUANZON MERCHANDISING CORP.    €
' €  including recording, or by information  storage  €€     Guanzon Bldg. Perez Blvd.     €
' €  and  retrieval  systems, without  prior written  €€           Dagupan City            €
' €  from the author.                                 €€  Tel No. 522-1085 ; 522-9275      €
' ºººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººº
'
' ==========================================================================================
'  Kalyptus [ 07/15/2017 09:42 am ]
'      Started creating of this object.
'€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€

Imports ggcLRTransaction
Imports ggcAppDriver

Public Class frmCheckClearing
    Private WithEvents poTrans As CheckReceived
    Private pnLoadx As Integer
    Private poControl As Control
    Private pnActiveRow As Integer

    Private Sub frmCheckClearing_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmCheckClearing_Activated")
        If pnLoadx = 1 Then

            poTrans = New CheckReceived(p_oAppDriver, CheckReceived.xePurposeChange, CheckReceived.xeCheckStatOpen)

            Call loadMaster(poTrans.EditMode)

            txtSeeks00.Text = poTrans.BranchName

            txtSeeks00.ReadOnly = True
            txtSeeks00.TabStop = False
            txtSeeks01.Focus()

            'If p_oAppDriver.ProductID <> "LRTrackr" Then
            '    txtSeeks00.ReadOnly = True
            '    txtSeeks00.TabStop = False
            '    txtSeeks01.Focus()
            'Else
            '    txtSeeks00.ReadOnly = False
            '    txtSeeks00.TabStop = True
            '    txtSeeks00.Focus()
            'End If

            pnLoadx = 2

        End If
    End Sub

    Private Sub frmCheckClearing_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Enter Then
            Dim loTxt As Control

            loTxt = Nothing
            If TypeOf poControl Is TextBox Then
                loTxt = CType(poControl, System.Windows.Forms.TextBox)
            ElseIf TypeOf poControl Is CheckBox Then
                loTxt = CType(poControl, System.Windows.Forms.CheckBox)
            ElseIf TypeOf poControl Is ComboBox Then
                loTxt = CType(poControl, System.Windows.Forms.ComboBox)
            End If

            '*********************
            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))

            If Mid(loTxt.Name, 1, 8) = "txtSeeks" Then
                Select Case loIndex
                    Case 0
                        Call poTrans.SearchBranch(loTxt.Text, False, True)
                        txtSeeks00.Text = poTrans.BranchName
                    Case 1, 2
                        If poTrans.SearchTransaction(loTxt.Text, IIf(loIndex = 1, False, True)) Then
                            Call loadMaster(poTrans.EditMode)
                        End If
                End Select
            End If

            '*********************
            If TypeOf poControl Is TextBox Then
                SelectNextControl(loTxt, True, True, True, True)
            End If
        End If
    End Sub

    Private Sub frmCheckClearing_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Debug.Print("frmCheckClearing_Load")
        If pnLoadx = 0 Then

            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "GotFocus", AddressOf txtSeeks_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "LostFocus", AddressOf txtSeeks_LostFocus)

            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)

            pnLoadx = 1
        End If
    End Sub

    Private Sub loadMaster(ByVal fnEditMode As xeEditMode)
        If fnEditMode = xeEditMode.MODE_READY Then
            txtField00.Text = poTrans.Master(0)
            txtField80.Text = poTrans.Master(80)
            txtField01.Text = poTrans.Master(1)
            txtField03.Text = poTrans.Master(3)
            txtField04.Text = poTrans.Master(4)
            txtField05.Text = Format(poTrans.Master(5), xsDECIMAL)

            cmbOther01.SelectedIndex = 0

            'iMac 2017.08.16 IFNULL
            txtSeeks01.Text = IFNull(poTrans.Detail(0, "xFullName"))
            txtSeeks02.Text = poTrans.Master(1)

            dgView.Rows.Clear()

            Dim lnRow As Integer
            For lnRow = 0 To poTrans.ItemNo - 1
                dgView.Rows.Add()
                dgView.Rows(lnRow).Cells(0).Value = Format(poTrans.Detail(lnRow, "dTransact"), xsDATE_SHORT)
                dgView.Rows(lnRow).Cells(1).Value = poTrans.Detail(lnRow, "cTranType")
                dgView.Rows(lnRow).Cells(2).Value = poTrans.Detail(lnRow, "sPRNoxxxx")
                dgView.Rows(lnRow).Cells(3).Value = Format(poTrans.Detail(lnRow, "nTranAmtx") + poTrans.Detail(lnRow, "nIntAmtxx") + poTrans.Detail(lnRow, "nPenaltyx"), xsDECIMAL)
            Next
            Call loadDetail(0)
        Else
            txtField00.Text = ""
            txtField80.Text = ""
            txtField01.Text = ""
            txtField03.Text = ""
            txtField04.Text = ""
            txtField05.Text = "0.00"

            Call loadDetail(-1)
            dgView.Rows.Add()
            dgView.Rows.Clear()

        End If

    End Sub

    Private Sub loadDetail(fnRow As Integer)

        pnActiveRow = fnRow

        If fnRow < 0 Then
            txtChild01.Text = ""
            txtChild04.Text = ""
            txtChild05.Text = ""
            txtChild06.Text = ""
            txtChild07.Text = ""
            txtChild09.Text = ""
            txtChild10.Text = ""
            txtChild11.Text = ""
            cmbChild12.SelectedIndex = -1
            txtChild14.Text = "0.00"
            txtChild15.Text = "0.00"
            txtChild16.Text = "0.00"
            txtChild17.Text = "0.00"
            txtOther02.Text = "0.00"
        Else
            txtChild01.Text = poTrans.Detail(fnRow, 1)
            txtChild04.Text = Format(poTrans.Detail(fnRow, 4), xsDATE_SHORT)
            txtChild05.Text = poTrans.Detail(fnRow, 5)
            txtChild06.Text = poTrans.Detail(fnRow, 6)
            txtChild07.Text = poTrans.Detail(fnRow, 7)
            txtChild09.Text = IFNull(poTrans.Detail(fnRow, 9), "")
            txtChild10.Text = IFNull(poTrans.Detail(fnRow, 10), "")
            txtChild11.Text = IFNull(poTrans.Detail(fnRow, 11), "")

            Dim lnType As Integer = Val(poTrans.Detail(fnRow, 12))
            cmbChild12.SelectedIndex = lnType

            txtChild14.Text = Format(poTrans.Detail(fnRow, 14), xsDECIMAL)
            txtChild15.Text = Format(poTrans.Detail(fnRow, 15), xsDECIMAL)
            txtChild16.Text = Format(poTrans.Detail(fnRow, 16), xsDECIMAL)
            txtChild17.Text = Format(poTrans.Detail(fnRow, 17), xsDECIMAL)
            txtOther02.Text = Format(poTrans.Detail(fnRow, "nTranAmtx") + poTrans.Detail(fnRow, "nIntAmtxx") + poTrans.Detail(fnRow, "nPenaltyx"), xsDECIMAL)
        End If
    End Sub

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button
        loChk = CType(sender, System.Windows.Forms.Button)

        Dim lnIndex As Integer
        lnIndex = Val(Mid(loChk.Name, 10))

        Select Case lnIndex
            Case 0 ' Exit
                Me.Dispose()
            Case 4 ' Save Transaction
                If poTrans.Master("cChckStat") = cmbOther01.SelectedIndex Then
                    MsgBox("No check status movement was detected...", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Check Clearing Form")
                    Exit Sub
                End If

                Select Case cmbOther01.SelectedIndex
                    Case 0 'ReOpen Check

                    Case 1 'Clear Check
                        If Not IsDate(txtOther00.Text) Then
                            MsgBox("Please indicate the OR Date...", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Check Clearing Form")
                            Exit Sub
                        End If

                        Dim lnCtr As Integer
                        For lnCtr = 0 To poTrans.ItemNo - 1
                            If poTrans.Detail(lnCtr, "sORNoxxxx") = "" Then
                                MsgBox("Some detail does not have an OR No...", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Check Clearing Form")
                                Exit Sub
                            End If
                        Next

                        If poTrans.ClearCheck(Convert.ToDateTime(txtOther00.Text)) Then
                            MsgBox("The check was tagged as CLEARED sucessfully...", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Check Clearing Form")
                            Call loadMaster(poTrans.EditMode)
                            'Call loadMaster(xeEditMode.MODE_UNKNOWN)
                        Else
                            MsgBox("Unable to tag the check as CLEARED...", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Check Clearing Form")
                        End If

                    Case 2 'Bounce Check
                        If Not IsDate(txtOther00.Text) Then
                            MsgBox("Please indicate the transaction Date...", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Check Clearing Form")
                            Exit Sub
                        End If

                        If poTrans.BounceCheck(Convert.ToDateTime(txtOther00.Text)) Then
                            MsgBox("The check was tagged as BOUNCED sucessfully...", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Check Clearing Form")
                            Call loadMaster(poTrans.EditMode)
                        Else
                            MsgBox("Unable to tag the check as BOUNCED...", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Check Clearing Form")
                        End If
                    Case 3 'Cancel Check
                        If Not IsDate(txtOther00.Text) Then
                            MsgBox("Please indicate the transaction Date...", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Check Clearing Form")
                            Exit Sub
                        End If

                        If poTrans.CancelTransaction(Convert.ToDateTime(txtOther00.Text)) Then
                            MsgBox("The check was tagged as CANCELLED sucessfully...", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Check Clearing Form")
                            Call loadMaster(poTrans.EditMode)
                        Else
                            MsgBox("Unable to tag the check as CANCELLED...", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Check Clearing Form")
                        End If
                    Case 4 'Hold Check
                        If Not IsDate(txtOther00.Text) Then
                            MsgBox("Please indicate the transaction Date...", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Check Clearing Form")
                            Exit Sub
                        End If
                        If poTrans.BounceCheck(Convert.ToDateTime(txtOther00.Text)) Then
                            MsgBox("The check was tagged as HOLD sucessfully...", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Check Clearing Form")
                            Call loadMaster(poTrans.EditMode)
                        Else
                            MsgBox("Unable to tag the check as HOLD...", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Check Clearing Form")
                        End If
                End Select
            Case 3 ' Browse Transaction
                If Not poControl Is Nothing Then
                    Dim loTxt As TextBox
                    loTxt = CType(poControl, System.Windows.Forms.TextBox)

                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))

                    Select Case loIndex
                        Case 0
                            Call poTrans.SearchBranch(loTxt.Text, False, True)
                            txtSeeks00.Text = poTrans.BranchName
                        Case 1, 2
                            If poTrans.SearchTransaction(loTxt.Text, IIf(loIndex = 1, False, True)) Then
                                Call loadMaster(poTrans.EditMode)
                            End If
                    End Select
                End If
        End Select
    End Sub

    Private Sub txtSeeks_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        poControl = loTxt
        loTxt.BackColor = Color.Azure
        loTxt.SelectAll()
    End Sub

    Private Sub txtSeeks_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        If loTxt.ReadOnly Then
            loTxt.BackColor = SystemColors.Control
        Else
            loTxt.BackColor = SystemColors.Window
        End If
    End Sub

    Private Sub txtOther00_Validated(sender As Object, e As System.EventArgs) Handles txtOther00.Validated
        If IsDate(txtOther00.Text) Then
            txtOther00.Text = Format(CDate(txtOther00.Text), xsDATE_SHORT)
        Else
            txtOther00.Text = ""
        End If
    End Sub

    Private Sub txtChild05_Validated(sender As Object, e As System.EventArgs) Handles txtChild05.Validated
        If poTrans.EditMode = xeEditMode.MODE_READY Then
            If poTrans.ItemNo > 0 Then
                poTrans.Detail(pnActiveRow, 5) = txtChild05.Text
            End If
        End If
    End Sub

    Private Sub dgView_Click(sender As Object, e As System.EventArgs) Handles dgView.Click
        With dgView
            If Not IsNothing(.CurrentRow) Then
                If .CurrentRow.Index >= 0 Then
                    Call loadDetail(.CurrentRow.Index)
                End If
            End If

            'set focus to txtItems if textbox is enabled
            If txtChild05.Enabled Then
                txtChild05.Focus()
            End If
        End With
    End Sub
End Class

