'€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€
' Guanzon Software Engineering Group
' Guanzon Group of Companies
' Perez Blvd., Dagupan City
'
'     AR Payment Entry Form - PR
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
'  Kalyptus [ 07/14/2017 09:11 am ]
'      Started creating of this object.
'€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€

Imports ggcLRTransaction
Imports ggcAppDriver

Public Class frmARPaymentEntryPR
    Private WithEvents poTrans As ARPayment_PR
    Private pnLoadx As Integer
    Private poControl As Control
    Private p_cTranType As String

    Public Property TranType() As String
        Get
            Return p_cTranType
        End Get
        Set(ByVal value As String)
            p_cTranType = value
        End Set
    End Property

    Private Sub frmARPaymentEntryPR_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmARPaymentEntryPR_Activated")
        If pnLoadx = 1 Then
            poTrans = New ARPayment_PR(p_oAppDriver, p_cTranType)

            Me.Text = "MC AR Payment Entry(PR)"

            Select Case p_cTranType
                Case "2"
                    Me.Text = Me.Text & " - Monthly Payment"
                Case "3"
                    Me.Text = Me.Text & " - Cash Balance"
                Case "4"
                    Me.Text = Me.Text & " - Down Balance"
            End Select

            If poTrans.NewTransaction() Then
                Call loadMaster(Me.Panel1)
                Call loadMaster(Me.Panel5)
                Call loadMaster(Me.Panel2)

                Call showEditMode(Me, xeEditMode.MODE_ADDNEW)
            End If

            txtSeeks00.Text = poTrans.BranchName
            If p_oAppDriver.ProductID <> "LRTrackr" Then
                txtSeeks00.ReadOnly = True
                txtSeeks00.TabStop = False
                txtField01.Focus()
            Else
                txtSeeks00.ReadOnly = False
                txtSeeks00.TabStop = True
                txtSeeks00.Focus()
            End If

            pnLoadx = 2

        End If
    End Sub

    Private Sub frmARPaymentEntryPR_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtField" Then
                Select Case loIndex
                    Case 80, 4, 97, 98
                        Call poTrans.SearchMaster(loIndex, loTxt.Text)
                End Select
            End If

            '*********************
            If TypeOf poControl Is TextBox Then
                SelectNextControl(loTxt, True, True, True, True)
            End If
        End If
    End Sub

    Private Sub frmARPaymentEntryPR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Debug.Print("frmARPaymentEntryPR_Load")
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
                            Case 1
                                If IsDate(poTrans.Master(loIndex)) Then
                                    loTxt.Text = Format(poTrans.Master(loIndex), "MMMM dd, yyyy")
                                Else
                                    loTxt.Text = ""
                                End If
                            Case 8, 10, 11, 82 To 90
                                If IsNumeric(poTrans.Master(loIndex)) Then
                                    loTxt.Text = Format(poTrans.Master(loIndex), xsDECIMAL)
                                Else
                                    loTxt.Text = "0.00"
                                End If
                            Case Else
                                loTxt.Text = IIf(poTrans.EditMode = xeEditMode.MODE_UNKNOWN, "", poTrans.Master(loIndex))
                        End Select
                    End If 'LCase(Mid(loTxt.Name, 1, 8)) = "txtfield"
                ElseIf (TypeOf loTxt Is ComboBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    Dim loCombo As ComboBox = CType(loTxt, ComboBox)
                    If LCase(Mid(loTxt.Name, 1, 8)) = "cmbfield" Then
                        If poTrans.EditMode = xeEditMode.MODE_UNKNOWN Then
                            loCombo.SelectedIndex = -1
                        Else
                            loCombo.SelectedIndex = poTrans.Master(loIndex) - 2
                        End If
                    End If
                End If '(TypeOf loTxt Is TextBox)
            End If 'If loTxt.HasChildren
        Next 'loTxt In loControl.Controls

        If IsNumeric(txtField08.Text) And IsNumeric(txtField11.Text) Then
            txtOther01.Text = Format(CDec(txtField08.Text) + CDec(txtField11.Text), xsDECIMAL)
        Else
            txtOther01.Text = "0.00"
        End If
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
                                loBtn.Visible = IIf(loIndex = 0 Or loIndex = 1, False, True)
                            Case Else
                                loBtn.Visible = IIf(loIndex = 0 Or loIndex = 1, True, False)
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
            If loIndex = 4 Then
                txtField80.Text = poTrans.Master(80)
            End If
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
            Case 1 ' New
                If poTrans.NewTransaction Then
                    Call loadMaster(Me.Panel1)
                    Call loadMaster(Me.Panel5)
                    Call loadMaster(Me.Panel2)
                    Call showEditMode(Me, xeEditMode.MODE_ADDNEW)
                    txtField01.Focus()
                End If
            Case 2 ' Search
                Dim loIndex As Integer
                loIndex = Val(Mid(poControl.Name, 10))

                If loIndex = 4 Or loIndex = 80 Or loIndex = 97 Or loIndex = 98 Then
                    poTrans.SearchMaster(loIndex, poControl.Text)
                ElseIf loIndex = 0 Then
                    poTrans.SearchBranch(poControl.Text, False, True)
                End If
            Case 3 ' Cancel Update
                If MsgBox("Do you really want to discard all changes?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "LR Payment Entry") = MsgBoxResult.Yes Then
                    poTrans.NewTransaction()
                    Call loadMaster(Me.Panel1)
                    Call loadMaster(Me.Panel5)
                    Call loadMaster(Me.Panel2)
                    showEditMode(Me, xeEditMode.MODE_READY)
                    cmdButton01.Focus()
                End If
            Case 4 'Save confirmation
                If CDbl(txtField08.Text) <= 0.0# And CDbl(txtField11.Text) <= 0.0# Then
                    MsgBox("No payment was detected!!" & vbCrLf & _
                            "Pls check your entry then try again!!!")
                    Exit Sub
                End If

                Dim loFrmx As ggcLRTransaction.frmReceipt
                loFrmx = New ggcLRTransaction.frmReceipt
                loFrmx.AppDriver = p_oAppDriver

                loFrmx.EntryType = "1"

                loFrmx.Text_ORNo = poTrans.Master("sReferNox")
                loFrmx.Text_AcctNmbr = poTrans.Master("sAcctNmbr")
                loFrmx.Text_ClientNm = poTrans.Master("sClientNm")
                loFrmx.Text_Addressx = poTrans.Master("sAddressX")
                loFrmx.Text_CashAmount = poTrans.Master("nAmountxx") + poTrans.Master("nPenaltyx")
                loFrmx.TranTotal = poTrans.Master("nAmountxx") + poTrans.Master("nPenaltyx")

                loFrmx.ShowDialog()
                If loFrmx.Cancelled Then Exit Sub

                poTrans.Master("sReferNox") = loFrmx.Text_ORNo

                poTrans.CheckInfo("sCheckNox") = loFrmx.Text_CheckNo
                poTrans.CheckInfo("sAcctNoxx") = loFrmx.Text_BnkActNo
                poTrans.CheckInfo("sBankIDxx") = loFrmx.BankID
                poTrans.CheckInfo("sBankName") = loFrmx.Text_BankName
                poTrans.CheckInfo("sCheckDte") = loFrmx.Text_CheckDate
                poTrans.CheckInfo("nCheckAmt") = loFrmx.Text_CheckAmount

                If poTrans.SaveTransaction Then
                    MsgBox("AR Payment was save successfully!", MsgBoxStyle.Information, "AR Payment Entry")
                    If MsgBox("Do you want POST the transaction", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirmation") = MsgBoxResult.Ok Then
                        If Not poTrans.PostTransaction Then
                            MsgBox("Unable to POST the AR Payment!", MsgBoxStyle.Information, "AR Payment Entry")
                        End If
                    End If

                    showEditMode(Me, xeEditMode.MODE_READY)
                    cmdButton01.Focus()
                Else
                    MsgBox("Unable to save AR Payment!", MsgBoxStyle.Information, "AR Payment Entry")
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

    Private Sub poTrans_MasterRetrieved(Index As Integer, Value As Object) Handles poTrans.MasterRetrieved
        Dim lnCtr As Integer
        Dim loTxt As TextBox
        'Find TextBox with specified name
        loTxt = CType(FindTextBox(Me, "txtField" & Format(Index, "00")), TextBox)

        Select Case Index
            Case 80
                txtField04.Text = poTrans.Master(4)
                For lnCtr = 80 To 95
                    loTxt = CType(FindTextBox(Me, "txtField" & Format(lnCtr, "00")), TextBox)
                    Select Case lnCtr
                        Case 82 To 90
                            loTxt.Text = Format(poTrans.Master(lnCtr), xsDECIMAL)
                        Case Else
                            loTxt.Text = poTrans.Master(lnCtr)
                    End Select
                Next
            Case 8, 11
                loTxt.Text = Format(Value, xsDECIMAL)
                txtOther01.Text = Format(CDec(txtField08.Text) + CDec(txtField11.Text), xsDECIMAL)
            Case 10
                loTxt.Text = Format(Value, xsDECIMAL)
            Case 1
                loTxt.Text = Format(Value, xsDATE_MEDIUM)
            Case Else
                loTxt.Text = Value
        End Select
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        p_cTranType = "2"
    End Sub
End Class