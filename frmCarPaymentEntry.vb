'€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€
' Guanzon Software Engineering Group
' Guanzon Group of Companies
' Perez Blvd., Dagupan City
'
'    Car Payment Entry Form
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
'  Jheff [ 01/06/2018 10:57 am ]
'      Started creating of this object.
'€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€
Imports ggcLRTransaction.LRPayment_Car
Imports ggcAppDriver

Public Class frmCarPaymentEntry
    Private WithEvents poTrans As ggcLRTransaction.LRPayment_Car
    Private pnLoadx As Integer
    Private poControl As Control
    Private p_cLoanType As String

    Public Property LoanType() As String
        Get
            Return p_cLoanType
        End Get
        Set(ByVal value As String)
            p_cLoanType = value
        End Set
    End Property

    Private Sub frmCarPaymentEntry_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmCarPaymentEntry_Activated")
        If pnLoadx = 1 Then
            If p_cLoanType = "1" Then
                Me.Text = "CAR Payment Entry"
            End If
            poTrans = New ggcLRTransaction.LRPayment_Car(p_oAppDriver, p_cLoanType)


            poTrans.LoanType = p_cLoanType
            txtSeeks00.Text = poTrans.BranchName

            If p_oAppDriver.ProductID <> "LRTrackr" Then
                txtSeeks00.ReadOnly = True
                txtSeeks00.TabStop = False
                txtField04.Focus()
            Else
                txtSeeks00.ReadOnly = False
                txtSeeks00.TabStop = True
                txtSeeks00.Focus()
            End If

            If poTrans.NewTransaction() Then
                Call loadMaster(Me.Panel1)
                Call loadMaster(Me.Panel5)
                Call loadMaster(Me.Panel2)

                Call showEditMode(Me, xeEditMode.MODE_ADDNEW)
            End If
            pnLoadx = 2
        End If
    End Sub

    Private Sub ArrowKeys_Keydown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Up, Keys.Down
                Select Case e.KeyCode
                    Case Keys.Down
                        SetNextFocus()
                    Case Keys.Up
                        SetPreviousFocus()
                End Select
        End Select
    End Sub

    Private Sub txtField_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
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
                    Case 80, 4, 90
                        Call poTrans.SearchMaster(loIndex, loTxt.Text)
                        Call loadMaster(Me.Panel1)
                        Call loadMaster(Me.Panel5)
                        Call loadMaster(Me.Panel2)
                End Select
            End If

            '*********************
            If TypeOf poControl Is TextBox Then
                SelectNextControl(loTxt, True, True, True, True)
            End If
        End If
    End Sub

    Private Sub frmCarPaymentEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Debug.Print("frmCarPaymentEntry_Load")
        If pnLoadx = 0 Then
            'Set event Handlers for txtField
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtField", "Validating", AddressOf txtField_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf txtField_KeyDown)
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
                            Case 7, 8, 9, 82, 86, 87, 91, 10
                                If IsNumeric(poTrans.Master(loIndex)) Then
                                    loTxt.Text = Format(poTrans.Master(loIndex), xsDECIMAL)
                                Else
                                    loTxt.Text = "0.00"
                                End If
                            Case Else
                                loTxt.Text = IIf(poTrans.EditMode = xeEditMode.MODE_UNKNOWN, "", poTrans.Master(loIndex))
                        End Select
                        txtOther10.Text = Format(poTrans.Master("nRebatesx"), xsDECIMAL)
                    End If 'LCase(Mid(loTxt.Name, 1, 8)) = "txtfield"
                End If '(TypeOf loTxt Is TextBox)
            End If 'If loTxt.HasChildren
        Next 'loTxt In loControl.Controls

        If poTrans.Master("nAcctTerm") > 0 Then
            txtField83.Text = IIf(poTrans.Master("nInterest") = poTrans.Master("nIntTotal"), 0.0, poTrans.Master("nInterest") / poTrans.Master("nAcctTerm"))
        Else
            txtField83.Text = "0.00"
        End If

        If IsNumeric(txtField86.Text) And IsNumeric(txtField83.Text) Then
            txtOther01.Text = Format(CDec(txtField86.Text) + CDec(txtField83.Text), xsDECIMAL)
            txtOther02.Text = Format(poTrans.Master(82) + (poTrans.Master("nInterest") - poTrans.Master("nIntTotal")), xsDECIMAL)
        Else
            txtOther01.Text = "0.00"
            txtOther02.Text = "0.00"
        End If
        txtOther03.Text = txtField09.Text
        txtOther04.Text = Format(Val(txtField07.Text) + Val(txtField08.Text) + Val(txtField09.Text), xsDECIMAL)
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
            Select Case loIndex
                Case 4
                    txtField80.Text = poTrans.Master(80)
                Case 9, 10, 91
                    If Not IsNumeric(loTxt.Text) Then
                        loTxt.Text = Format(poTrans.Master(loIndex), xsDECIMAL)
                    Else
                        loTxt.Text = Format(CDec(loTxt.Text), xsDECIMAL)
                        poTrans.Master(loIndex) = loTxt.Text
                    End If
            End Select
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
                    Call loadMaster(Me.Panel2)
                    Call loadMaster(Me.Panel5)
                    Call showEditMode(Me, xeEditMode.MODE_ADDNEW)
                End If
            Case 2 ' Search
                Dim loIndex As Integer
                loIndex = Val(Mid(poControl.Name, 10))
                If loIndex = 1 Then
                    poTrans.SearchMaster(loIndex, poControl.Text)
                ElseIf loIndex = 90 Then
                    poTrans.SearchMaster(loIndex, poControl.Text)
                ElseIf loIndex = 0 Then
                    poTrans.SearchBranch(poControl.Text, False, True)
                End If
            Case 3 ' Cancel Update
                If MsgBox("Do you really want to discard all changes?", MsgBoxStyle.Information, "Car Payment Entry") Then
                    poTrans.NewTransaction()
                    Call loadMaster(Me.Panel1)
                    Call loadMaster(Me.Panel2)
                    Call loadMaster(Me.Panel5)
                    showEditMode(Me, xeEditMode.MODE_READY)
                End If
            Case 4 'Save confirmation
                Dim loFrmx As ggcLRTransaction.frmReceipt
                loFrmx = New ggcLRTransaction.frmReceipt
                loFrmx.AppDriver = p_oAppDriver

                loFrmx.Text_ORNo = poTrans.Master("sReferNox")
                loFrmx.Text_AcctNmbr = poTrans.Master("sAcctNmbr")
                loFrmx.Text_ClientNm = poTrans.Master("sClientNm")
                loFrmx.Text_Addressx = poTrans.Master("sAddressX")
                loFrmx.Text_CashAmount = poTrans.Master(91) + poTrans.Master("nPenaltyX")
                loFrmx.TranTotal = poTrans.Master(91) + poTrans.Master("nPenaltyX")

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
                    MsgBox("Car Payment was save successfully!", MsgBoxStyle.Information, "Car Payment Entry")
                    If MsgBox("Do you want to print the transaction", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Car Payment Printing") = MsgBoxResult.Ok Then
                        If Not poTrans.PrintTrans Then
                            MsgBox("Unable to print Car Payment!", MsgBoxStyle.Information, "Car Payment Entry")
                        End If
                    End If

                    If MsgBox("Do you want to POST the transaction", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Car Payment Confirmation") = MsgBoxResult.Ok Then
                        If Not poTrans.PostTransaction Then
                            MsgBox("Unable to POST the Car Payment!", MsgBoxStyle.Information, "Car Payment Entry")
                        End If
                    End If

                    showEditMode(Me, xeEditMode.MODE_READY)
                    cmdButton01.Focus()
                Else
                    MsgBox("Unable to save Car Payment!", MsgBoxStyle.Information, "Car Payment Entry")
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
            Case 91
                loTxt.Text = Format(Value, xsDECIMAL)
            Case 80
                loTxt.Text = Value
                txtField04.Text = poTrans.Master(4)
                txtField81.Text = poTrans.Master(81)

                txtField82.Text = Format(poTrans.Master(82), xsDECIMAL)
                txtField87.Text = Format(poTrans.Master(87), xsDECIMAL)
                txtField86.Text = Format(poTrans.Master(86), xsDECIMAL)
                txtOther10.Text = Format(poTrans.Master("nRebatesx"), xsDECIMAL)

                If poTrans.Master("nAcctTerm") > 0 Then
                    txtField83.Text = IIf(poTrans.Master("nInterest") = poTrans.Master("nIntTotal"), 0.0, poTrans.Master("nInterest") / poTrans.Master("nAcctTerm"))
                Else
                    txtField83.Text = "0.00"
                End If
                txtOther01.Text = Format(CDec(txtField86.Text) + CDec(txtField83.Text), xsDECIMAL)
                txtOther02.Text = Format(poTrans.Master(82) + (poTrans.Master("nInterest") - poTrans.Master("nIntTotal")), xsDECIMAL)
            Case 7, 8, 9, 10
                loTxt.Text = Format(Value, xsDECIMAL)
                If Index = 9 Then
                    txtOther03.Text = Format(Value, xsDECIMAL)
                Else
                    'kalyptus - 2016.11.08 09:23am
                    'Format the entry after the 
                    txtField91.Text = Format(CDec(txtField91.Text), xsDECIMAL)
                End If
                txtOther04.Text = Format(poTrans.Master(7) + poTrans.Master(8) + poTrans.Master(9), xsDECIMAL)
            Case 1
                loTxt.Text = Format(Value, xsDATE_MEDIUM)
            Case Else
                loTxt.Text = Value
        End Select
    End Sub
End Class