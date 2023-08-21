'€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€
' Guanzon Software Engineering Group
' Guanzon Group of Companies
' Perez Blvd., Dagupan City
'
'     Check Received Reg Form
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
'  Kalyptus [ 07/21/2017 11:21 am ]
'      Started creating of this object.
'€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€

Imports ggcLRTransaction
Imports ggcAppDriver

Public Class frmCheckReg
    Private WithEvents poTrans As CheckReceived
    Private pnLoadx As Integer
    Private poControl As Control
    Private pnActiveRow As Integer

    Private Sub frmCheckReg_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmCheckReg_Activated")
        If pnLoadx = 1 Then

            poTrans = New CheckReceived(p_oAppDriver, CheckReceived.xePurposeView, -1)

            Call loadMaster(poTrans.EditMode)

            txtSeeks00.Text = poTrans.BranchName

            txtSeeks00.ReadOnly = True
            txtSeeks00.TabStop = False
            txtSeeks01.Focus()

            pnLoadx = 2

        End If
    End Sub

    Private Sub frmCheckReg_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub frmCheckReg_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Debug.Print("frmCheckReg_Load")
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

            If IsDate(poTrans.Master(9)) Then
                txtField09.Text = Format(poTrans.Master(9), xsDATE_MEDIUM)
            Else
                txtField09.Text = ""
            End If

            txtSeeks01.Text = poTrans.Detail(0, "xFullName")
            txtSeeks02.Text = poTrans.Master(1)

            Select Case poTrans.Master("cChckStat")
                Case CheckReceived.xeCheckStatCleared
                    txtStatus.Text = "CLEARED"
                Case CheckReceived.xeCheckStatBounce
                    txtStatus.Text = "BOUNCED"
                Case CheckReceived.xeCheckStatCancelled
                    txtStatus.Text = "CANCELLED"
                Case CheckReceived.xeCheckStatHold
                    txtStatus.Text = "HOLD"
                Case CheckReceived.xeCheckStatOpen
                    If poTrans.Master("cDepositd") = "0" Then
                        txtStatus.Text = "OPEN"
                    Else
                        txtStatus.Text = "DEPOSITED"
                    End If
            End Select

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

            txtStatus.Text = "UNKNOWN"

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
            txtChild06.Text = poTrans.Detail(fnRow, 6)
            txtChild07.Text = poTrans.Detail(fnRow, 7)
            txtChild09.Text = poTrans.Detail(fnRow, 9)
            txtChild10.Text = poTrans.Detail(fnRow, 10)
            txtChild11.Text = poTrans.Detail(fnRow, 11)

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

    Private Sub dgView_Click(sender As Object, e As System.EventArgs) Handles dgView.Click
        With dgView
            If Not IsNothing(.CurrentRow) Then
                If .CurrentRow.Index >= 0 Then
                    Call loadDetail(.CurrentRow.Index)
                End If
            End If
        End With
    End Sub
End Class