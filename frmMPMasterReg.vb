'€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€
' Guanzon Software Engineering Group
' Guanzon Group of Companies
' Perez Blvd., Dagupan City
'
'     LR Master - Car Trade Maintenance Form
'
' Copyright 2018 and Beyond
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
'  Jheff [ 04/19/2018 04:38 pm ]
'      Started creating of this object.
'€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€
Imports ggcLRTransaction
Imports ggcAppDriver

Public Class frmMPMasterReg
    Private WithEvents poTrans As ggcLRTransaction.LRMaster
    Private pnLoadx As Integer
    Private poControl As Control
    Private pnStatus As String = 0

    Public Property Status As Integer
        Get
            Return pnStatus
        End Get
        Set(ByVal value As Integer)
            'If Product ID is LR then do allow changing of Branch
            pnStatus = value
        End Set
    End Property

    Private Sub frmLRMasterReg_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmMPMasterReg_Activated")
        If pnLoadx = 1 Then
            If pnStatus = 0 Then
                Me.Text = "MP AR Account - Active"
            Else : pnStatus = 1234
                Me.Text = "MP AR Account - Closed"
            End If
            poTrans = New ggcLRTransaction.LRMaster(p_oAppDriver, pnStatus)
            Call loadMaster(Me.Panel1)
            txtSeeks00.Text = IIf(poTrans.Master("sAcctNmbr") = "0", "", poTrans.Master("sAcctNmbr"))
            txtSeeks01.Text = IIf(poTrans.Master("sClientNm") = "0", "", poTrans.Master("sClientNm"))

            If p_oAppDriver.UserLevel = xeUserRights.ENGINEER Then
                txtField17.ReadOnly = False
                txtField17.TabStop = True
                txtField10.ReadOnly = False
                txtField10.TabStop = True
                'txtField15.ReadOnly = False
                'txtField15.TabStop = True
            End If

            Call showEditMode(Me, xeEditMode.MODE_READY)
            pnLoadx = 2
        End If
    End Sub

    Private Sub frmLRMasterReg_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
                Call poTrans.SearchTransaction(loTxt.Text, IIf(loIndex = 1, False, True))
                Call loadMaster(Me.Panel1)

                txtSeeks00.Text = IIf(poTrans.Master("sAcctNmbr") = "0", "", poTrans.Master("sAcctNmbr"))
                txtSeeks01.Text = IIf(poTrans.Master("sClientNm") = "0", "", poTrans.Master("sClientNm"))
            ElseIf Mid(loTxt.Name, 1, 8) = "txtField" Then
                If loIndex = 87 Then
                    poTrans.SearchMaster(loIndex, loTxt.Text)
                End If
            End If

            '*********************
            If TypeOf poControl Is TextBox Then
                SelectNextControl(loTxt, True, True, True, True)
            End If
        End If
    End Sub

    Private Sub frmLRMasterReg_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Debug.Print("frmLRMasterReg_Load")
        If pnLoadx = 0 Then

            'Set event Handlers for txtField
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtField", "Validating", AddressOf txtField_Validating)

            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "GotFocus", AddressOf txtSeeks_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "LostFocus", AddressOf txtSeeks_LostFocus)

            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)

            pnLoadx = 1
        End If
    End Sub

    Private Sub loadMaster(ByVal loControl As Control)
        Dim loTxt As Control

        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadMaster(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtfield" Then
                        Select Case loIndex
                            Case 7, 16, 18, 22, 32
                                If IsDate(poTrans.Master(loIndex)) Then
                                    loTxt.Text = Format(poTrans.Master(loIndex), "MMMM dd, yyyy")
                                Else
                                    loTxt.Text = ""
                                End If
                            Case 15, 20, 8 To 14, 23 To 29, 19, 36, 21
                                If IsNumeric(poTrans.Master(loIndex)) And poTrans.Master(loIndex) <> vbEmpty Then
                                    loTxt.Text = Format(poTrans.Master(loIndex), xsDECIMAL)
                                Else
                                    loTxt.Text = "0.00"
                                End If


                                If loIndex = 19 Then
                                    If poTrans.EditMode = xeEditMode.MODE_UNKNOWN Then
                                        txtOther01.Text = "0.00"
                                        txtOther02.Text = "0.00"
                                    Else
                                        If poTrans.Master("sCollatID") <> "" Then
                                            txtOther01.Text = Format(poTrans.Master("nInterest") / poTrans.Master("nAcctTerm"), xsDECIMAL)
                                        Else
                                            txtOther01.Text = "0.00"
                                        End If
                                        txtOther02.Text = Format(CDec(txtField19.Text) + CDec(txtOther01.Text), xsDECIMAL)
                                    End If
                                End If

                            Case 30
                                If poTrans.EditMode = xeEditMode.MODE_UNKNOWN Then
                                    loTxt.Text = ""
                                Else
                                    'TODO: DETERMINE THE DIFFERENT RATE VALUE 
                                    loTxt.Text = IIf(poTrans.Master(loIndex) = "0", "", "GET RATE")
                                End If
                            Case 31
                                If poTrans.EditMode = xeEditMode.MODE_UNKNOWN Then
                                    loTxt.Text = ""
                                Else
                                    'TODO: DETERMINE THE DIFFERENT ACCOUNT STATUS
                                    loTxt.Text = IIf(poTrans.Master(loIndex) = 0, "OPEN", "CLOSED")
                                End If
                            Case 33
                                If poTrans.EditMode = xeEditMode.MODE_UNKNOWN Then
                                    loTxt.Text = ""
                                Else
                                    loTxt.Text = IIf(poTrans.Master(loIndex) = 0, "INACTIVE", "ACTIVE")
                                End If
                            Case Else
                                loTxt.Text = IIf(poTrans.EditMode = xeEditMode.MODE_UNKNOWN, "", poTrans.Master(loIndex))
                        End Select
                    End If 'LCase(Mid(loTxt.Name, 1, 8)) = "txtfield"
                End If '(TypeOf loTxt Is TextBox)
            End If 'If loTxt.HasChildren
        Next 'loTxt In loControl.Controls
    End Sub

    'Handles GotFocus Events for txtField & txtField
    Private Sub txtField_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        poControl = loTxt

        loTxt.BackColor = Color.Azure
        loTxt.SelectAll()
    End Sub

    'Handles LostFocus Events for txtField & txtField
    Private Sub txtField_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        If loTxt.ReadOnly Then
            loTxt.BackColor = SystemColors.Control
        Else
            loTxt.BackColor = SystemColors.Window
        End If
    End Sub

    'Handles Validating Events for txtField & txtField
    Private Sub txtField_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))
        If Mid(loTxt.Name, 1, 8) = "txtField" And loTxt.ReadOnly = False Then
            poTrans.Master(loIndex) = loTxt.Text
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
            Case 1
                If IIf(poTrans.Master("sClientNm") = "0", "", poTrans.Master("sClientNm")) <> "" Then
                    showEditMode(Me, xeEditMode.MODE_UPDATE)
                End If
            Case 2 ' Search
                Dim loIndex As Integer
                loIndex = Val(Mid(poControl.Name, 10))

                If poTrans.SearchTransaction(poControl.Text, IIf(loIndex = 1, False, True)) Then
                    Call loadMaster(Me.Panel1)
                    txtSeeks00.Text = IIf(poTrans.Master("sAcctNmbr") = "0", "", poTrans.Master("sAcctNmbr"))
                    txtSeeks01.Text = IIf(poTrans.Master("sClientNm") = "0", "", poTrans.Master("sClientNm"))
                End If
            Case 3 ' Exit
                If MsgBox("Do you really want to cancel the update?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "LR Master") = MsgBoxResult.Yes Then
                    showEditMode(Me, xeEditMode.MODE_READY)
                End If
            Case 4 ' Save
                If poTrans.SaveTransaction Then
                    MsgBox("Update was saved sucessfully...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "LR Master")
                Else
                    poTrans.OpenTransaction(poTrans.Master("sAcctNmbr"))
                    MsgBox("Unable to save the update...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "LR Master")
                End If
                showEditMode(Me, xeEditMode.MODE_READY)
            Case 5 ' Ledger
                Dim loFrm As New frmLRLedger
                loFrm.AccountNo = poTrans.Master("sAcctNmbr")
                loFrm.ClientName = poTrans.Master("sClientNm")
                loFrm.Address = poTrans.Master("sAddressx")
                loFrm.Company = poTrans.Master("sCompnyNm")
                loFrm.ShowDialog()
            Case 6 ' Recalculate
                If poTrans.EditMode = xeEditMode.MODE_READY Then
                    Dim loTrans As LRTrans = New LRTrans(p_oAppDriver)
                    p_oAppDriver.BeginTransaction()
                    If loTrans.Recalculate(poTrans.Master("sAcctNmbr")) Then
                        MsgBox("Account was recalculated sucessfully...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "LR Master")
                        'p_oAppDriver.RollBackTransaction()
                        p_oAppDriver.CommitTransaction()
                    Else
                        p_oAppDriver.RollBackTransaction()
                    End If
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

    Private Sub poTrans_MasterRetrieved(ByVal Index As Integer, ByVal Value As Object) Handles poTrans.MasterRetrieved
        Dim loTxt As TextBox
        'Find TextBox with specified name
        loTxt = CType(FindTextBox(Me, "txtField" & Format(Index, "00")), TextBox)

        Select Case Index
            Case 87
                loTxt.Text = Value
                'txtField88.Text = poTrans.Master(88)
                'txtField89.Text = poTrans.Master(89)
            Case 18
                loTxt.Text = Format(Value, "MMMM dd, yyyy")
            Case 9, 10, 31, 36
                loTxt.Text = Format(Value, xsDECIMAL)
            Case 19
                loTxt.Text = Format(Value, xsDECIMAL)
                If poTrans.EditMode = xeEditMode.MODE_UNKNOWN Then
                    txtOther01.Text = "0.00"
                    txtOther02.Text = "0.00"
                Else
                    If poTrans.Master("sCollatID") <> "" Then
                        txtOther01.Text = Format(poTrans.Master("nInterest") / poTrans.Master("nAcctTerm"), xsDECIMAL)
                    Else
                        txtOther01.Text = "0.00"
                    End If
                    txtOther02.Text = Format(CDec(txtField19.Text) + CDec(txtOther01.Text), xsDECIMAL)
                End If
            Case Else
                loTxt.Text = Value
        End Select
    End Sub

    Private Sub showEditMode(ByVal loControl As Control, ByVal fnEditMode As xeEditMode)
        Dim loBtn As Control

        For Each loBtn In loControl.Controls
            If loBtn.HasChildren Then
                Call showEditMode(loBtn, fnEditMode)
            Else
                If (TypeOf loBtn Is Button) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loBtn.Name, 10))
                    If LCase(Mid(loBtn.Name, 1, 9)) = "cmdbutton" Then
                        Select Case fnEditMode
                            Case xeEditMode.MODE_ADDNEW, xeEditMode.MODE_UPDATE
                                loBtn.Visible = IIf(loIndex = 3 Or loIndex = 4, True, False)
                            Case Else
                                loBtn.Visible = IIf(loIndex = 3 Or loIndex = 4, False, True)
                        End Select
                    End If 'LCase(Mid(loTxt.Name, 1, 9)) = "cmdButton"
                ElseIf (TypeOf loBtn Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loBtn.Name, 9))
                    If loBtn.TabStop Then
                        Select Case fnEditMode
                            Case xeEditMode.MODE_ADDNEW, xeEditMode.MODE_UPDATE
                                loBtn.Enabled = IIf(LCase(Mid(loBtn.Name, 1, 8)) = "txtfield", True, False)
                            Case Else
                                loBtn.Enabled = IIf(LCase(Mid(loBtn.Name, 1, 8)) = "txtfield", False, True)
                        End Select
                    End If
                End If '(TypeOf loTxt Is Button)
            End If 'If loTxt.HasChildren
        Next 'loTxt In loControl.Controls

        'kalyptus - 2017.05.20 05:00pm 
        'System Engieers are the only user able to use the recalculate...
        If Not p_oAppDriver.UserLevel = xeUserRights.ENGINEER Then
            cmdButton06.Visible = False
        End If
    End Sub
End Class