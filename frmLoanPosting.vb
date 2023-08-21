Imports MySql.Data.MySqlClient
Imports ggcLRTransaction
Imports ggcAppDriver

Public Class frmLoanPosting
    Private WithEvents p_oTrans As ggcLRTransaction.LRMasterCarNeo
    Private pnLoadx As Integer
    Private poControl As Control
    Private p_nEditMode As Integer
    Private p_AccountStatus As String

    Public Property AccountStatus() As String
        Get
            Return p_AccountStatus
        End Get
        Set(ByVal value As String)
            p_AccountStatus = value
        End Set
    End Property

    Private Sub frmLoanPosting_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmLoanPosting_Activated")
        If p_AccountStatus = "-1" Then
            'If p_AccountStatus = "10234" Then
            Me.Text = "CarTrade Posting/Confirmation"
        End If
        If pnLoadx = 1 Then
            p_oTrans.InitTransaction()
            p_oTrans.NewTransaction()
            Call newRecord()
            pnLoadx = 2
        End If
    End Sub

    Private Sub frmLoanPosting_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Enter Then
            Dim loTxt As Control
            loTxt = CType(sender, System.Windows.Forms.TextBox)

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
                    Case 0, 1
                        If p_oTrans.SearchTransaction(poControl.Text, False) = True Then
                            loadMaster()
                            setTextSeeks()
                        End If

                End Select
            End If

            '*********************
            If TypeOf poControl Is TextBox Then
                SelectNextControl(loTxt, True, True, True, True)
            End If
        End If
    End Sub

    Private Sub ArrowKeys_Keydown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Up, Keys.Down, Keys.Left, Keys.Right
                Select Case e.KeyCode
                    Case Keys.Down, Keys.Right
                        SetNextFocus()
                    Case Keys.Up, Keys.Left
                        SetPreviousFocus()
                End Select
        End Select
    End Sub

    Private Sub frmLoanPosting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Debug.Print("frmLoanPosting_Load")
        If pnLoadx = 0 Then
            p_oTrans = New ggcLRTransaction.LRMasterCarNeo(p_oAppDriver)
            p_oTrans.AccountStatus = p_AccountStatus
            'Set event Handlers for txtField
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtField", "Validating", AddressOf txtField_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtSeeks", "KeyDown", AddressOf frmLoanPosting_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf frmLoanPosting_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "GotFocus", AddressOf txtSeeks_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "LostFocus", AddressOf txtSeeks_LostFocus)
            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)
            pnLoadx = 1
        End If
    End Sub

    Private Sub loadMaster()
        Call loadEntry(Me.Panel1)
        Call loadEntry(Me.Panel3)
        TabControl1.SelectedIndex = 0
    End Sub

    Private Function newRecord() As Boolean
        txtField02.Focus()
        Call loadMaster()
        txtOther01.Text = "0.00"
        txtOther02.Text = "0.00"
        Return True
    End Function

    Private Sub loadEntry(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadEntry(loTxt)
            ElseIf (TypeOf loTxt Is TextBox) Then
                Dim loIndex As Integer
                loIndex = Val(Mid(loTxt.Name, 9))
                If LCase(Mid(loTxt.Name, 1, 8)) = "txtField" Then
                    Select Case loIndex
                        Case 1, 10, 12, 16
                            If IsDate(p_oTrans.Master(loIndex)) Then
                                loTxt.Text = Format(p_oTrans.Master(loIndex), "MMMM dd, yyyy")
                            End If
                        Case 5 To 9, 13, 14, 15, 18 To 23, 25, 29, 30
                            If IsNumeric(p_oTrans.Master(loIndex)) Then
                                loTxt.Text = Format(p_oTrans.Master(loIndex), xsDECIMAL)
                            Else
                                loTxt.Text = "0.00"
                            End If
                        Case 24
                            If IsNumeric(p_oTrans.Master(loIndex)) Then
                                loTxt.Text = p_oTrans.Master("nPrincipl") + p_oTrans.Master("nInterest")
                                loTxt.Text = CDec(loTxt.Text) - ((p_oTrans.Master("nPrincipl") - IFNull(p_oTrans.Master("nABalance"), 0)) + IFNull(p_oTrans.Master("nIntTotal"), 0))
                                loTxt.Text = Format(CDec(loTxt.Text), xsDECIMAL)
                            Else
                                loTxt.Text = "0.00"
                            End If
                        Case 31
                            If IsDBNull(p_oTrans.Master("cAcctStat")) Then
                                loTxt.Text = "UNKNOWN"
                            Else
                                loTxt.Text = IIf(p_oTrans.Master("cAcctStat") = "0", "ACTIVE", "INACTIVE")
                            End If
                        Case 27
                            If IsDBNull(p_oTrans.Master("cAcctStat")) Then
                                loTxt.Text = "UNKNOWN"
                            Else
                                loTxt.Text = IIf(p_oTrans.Master("cAcctStat") = "0", "ACTIVE", "INACTIVE")
                            End If
                        Case 34
                            loTxt.Text = IFNull(p_oTrans.Detail("sEngineNo"), "")
                        Case 35
                            loTxt.Text = IFNull(p_oTrans.Detail("sFrameNox"), "")
                        Case 36
                            loTxt.Text = IFNull(p_oTrans.Detail("sBrandNme"), "")
                        Case 37
                            loTxt.Text = IFNull(p_oTrans.Detail("sModelNme"), "")
                        Case 38
                            loTxt.Text = IFNull(p_oTrans.Detail("sColorNme"), "")
                        Case 39
                            loTxt.Text = IFNull(p_oTrans.Detail("sFileNoxx"), "")
                        Case 40
                            loTxt.Text = IFNull(p_oTrans.Detail("sCRENoxxx"), "")
                        Case 41
                            loTxt.Text = IFNull(p_oTrans.Detail("sCRNoxxxx"), "")
                        Case 42
                            loTxt.Text = IFNull(p_oTrans.Detail("sPlateNoP"), "")
                        Case 43
                            loTxt.Text = IFNull(p_oTrans.Detail("dRegister"), Format(p_oAppDriver.getSysDate, "MMMM dd, yyyy"))
                            If Not loTxt.Text = "" Then
                                p_oTrans.Detail("dRegister") = loTxt.Text
                                loTxt.Text = Format(p_oTrans.Detail("dRegister"), "MMMM dd, yyyy")
                            End If
                        Case 44
                            loTxt.Text = IFNull(p_oTrans.Detail("nYearModl"), Format(p_oAppDriver.getSysDate, "yyyy"))
                        Case 46
                            If IFNull(p_oTrans.Detail("nSubsidze")) Then
                                loTxt.Text = Format(p_oTrans.Detail("nSubsidze"), xsDECIMAL)
                            Else
                                loTxt.Text = "0.00"
                            End If
                        Case 47
                            If IFNull(p_oTrans.Detail("nSubCrdtd")) Then
                                loTxt.Text = Format(p_oTrans.Detail("nSubCrdtd"), xsDECIMAL)
                            Else
                                loTxt.Text = "0.00"
                            End If
                        Case 48
                            If IFNull(p_oTrans.Detail("nInctvAmt")) Then
                                loTxt.Text = Format(p_oTrans.Detail("nInctvAmt"), xsDECIMAL)
                            Else
                                loTxt.Text = "0.00"
                            End If
                        Case 49
                            If IFNull(p_oTrans.Detail("nIncntPdx")) Then
                                loTxt.Text = Format(p_oTrans.Detail("nIncntPdx"), xsDECIMAL)
                            Else
                                loTxt.Text = "0.00"
                            End If
                        Case 50
                            If IFNull(p_oTrans.Detail("nInsAmtxx")) Then
                                loTxt.Text = Format(p_oTrans.Detail("nInsAmtxx"), xsDECIMAL)
                            Else
                                loTxt.Text = "0.00"
                            End If
                        Case 51
                            If IFNull(p_oTrans.Detail("nInsAmtPd")) Then
                                loTxt.Text = Format(p_oTrans.Detail("nInsAmtPd"), xsDECIMAL)
                            Else
                                loTxt.Text = "0.00"
                            End If
                        Case Else
                            loTxt.Text = IFNull(p_oTrans.Master(loIndex), "")
                    End Select
                End If
                txtOther02.Text = Format(Val(p_oTrans.Master("nInterest")) / Val(p_oTrans.Master("nAcctTerm")), xsDECIMAL)
                txtOther01.Text = Format(Val(p_oTrans.Master("nMonAmort")) + Val(p_oTrans.Master("nInterest")) / Val(p_oTrans.Master("nAcctTerm")), xsDECIMAL)
            End If
        Next
    End Sub

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

    ''Handles Validating Events for txtField & txtField
    Private Sub txtField_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))
        If Mid(loTxt.Name, 1, 8) = "txtField" And loTxt.ReadOnly = False Then
            p_oTrans.Master(loIndex) = loTxt.Text
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
            Case 1 ' Cancel
                If Not p_oTrans.Master("sAcctNmbr") = "" And Not p_oTrans.Master("sClientNm") = "" Then
                    If txtField27.Text = "UNKNOWN" Then
                        If MsgBox("Do you really want to cancel this transaction?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "CarTrade Entry") = MsgBoxResult.Yes Then
                            MsgBox("Loan was cancelled successfully!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "CarTrade Entry")
                            p_oTrans.CancelTransaction()
                            p_oTrans.NewTransaction()
                            loadMaster()
                            Call ClearTextBoxes()
                        End If
                    Else
                        MessageBox.Show("Unable to cancel transaction" + Environment.NewLine + "Already posted!", "Error!",
                                          MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Else
                    MessageBox.Show("Please select a transaction to cancel", "Error!",
                                          MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Case 2 ' Approved
                If Not p_oTrans.Master("sAcctNmbr") = "" And Not p_oTrans.Master("sClientNm") = "" Then
                    If MsgBox("Do you really want to post this transaction?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "CarTrade Entry") = MsgBoxResult.Yes Then
                        If p_oTrans.PostTransaction Then
                            MsgBox("Loan was posted successfully!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "CarTrade Entry")
                            Call p_oTrans.NewTransaction()
                            Call ClearTextBoxes()
                            loadMaster()
                        End If
                    End If
                Else
                    MessageBox.Show("Please select a transaction to post", "Error!",
                                          MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Case 3 ' Browse
                If p_oTrans.SearchTransaction("", False) = True Then
                    loadMaster()
                    p_nEditMode = xeEditMode.MODE_READY
                    setTextSeeks()
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

    Public Sub ClearTextBoxes()
        txtSeeks00.Text = ""
        txtSeeks01.Text = ""
        txtField00.Text = ""
        txtField01.Text = Format(p_oAppDriver.getSysDate(), "MMMM dd, yyyy")
        txtField02.Text = ""
        txtField03.Text = ""
        txtField34.Text = ""
        txtField35.Text = ""
        txtField36.Text = ""
        txtField37.Text = ""
        txtField38.Text = ""
        txtField39.Text = ""
        txtField40.Text = ""
        txtField41.Text = ""
        txtField42.Text = ""
        txtField43.Text = Format(p_oAppDriver.getSysDate(), "MMMM dd, yyyy")
        txtField44.Text = Format(p_oAppDriver.getSysDate(), "yyyy")
        txtOther01.Text = "0.00"
        txtOther02.Text = "0.00"
    End Sub

    Public Function setTextSeeks() As Boolean
        txtSeeks00.Text = p_oTrans.Master("sAcctNmbr")
        txtSeeks01.Text = txtField02.Text
    End Function

    Private Sub txtSeeks_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        loTxt.BackColor = SystemColors.Window

    End Sub

    Private Sub poTrans_MasterRetrieved(ByVal Index As Integer, ByVal Value As Object) Handles p_oTrans.MasterRetrieved
        Dim loTxt As TextBox
        'ind TextBox with specified name
        loTxt = CType(FindTextBox(Me, "txtField" & Format(Index, "00")), TextBox)
        Select Case Index
            Case 43
                loTxt.Text = Format(CDate(Value), "MMMM dd, yyyy")
            Case Else
                loTxt.Text = IFNull(Value, "")
        End Select
    End Sub

End Class