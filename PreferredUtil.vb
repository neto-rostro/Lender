Module PreferredUtil
    Public Sub setTranStat(ByVal fsTransTat As String, ByVal lblField As Label)
        Select fsTransTat
            Case 0 'Open
                lblField.Text = "OPEN"
            Case 1 'Verified
                lblField.Text = "VERIFIED"
            Case 2
                lblField.Text = "POSTED"
            Case 3 'Disapproved
                lblField.Text = "CANCELLED"
            Case 4 'Voided
                lblField.Text = "VOIDED"
            Case Else
                lblField.Text = "UNKNOWN"
        End Select
    End Sub

    Public Sub initBank(ByVal listView As ListView)
        'this will add items to list view.
        With listView
            .Columns.Clear()
            .Items.Clear()
            listView.View = View.Details
            .Columns.Add("No", 30, HorizontalAlignment.Center)
            .Columns.Add("Bank Name", 100, HorizontalAlignment.Center)
            .Columns.Add("Branch", 100, HorizontalAlignment.Center)
            .Columns.Add("Account No", 100, HorizontalAlignment.Center)
        End With
    End Sub

    Public Sub setTransTat(ByVal nStat As Integer, ByVal lblStatus As Label)
        Select Case nStat
            Case 0
                lblStatus.Text = "OPEN"
            Case 1
                lblStatus.Text = "CLOSED"
            Case 2
                lblStatus.Text = "POSTED"
            Case 3
                lblStatus.Text = "CANCELLED"
            Case 4
                lblStatus.Text = "VOID"
            Case Else
                lblStatus.Text = "UNKNOWN"
        End Select
    End Sub


    Public Sub initReference(ByVal listView As ListView)
        'this will add items to list view.
        With listView
            .Columns.Clear()
            .Items.Clear()
            listView.View = View.Details
            .Columns.Add("No", 30, HorizontalAlignment.Center)
            .Columns.Add("Name", 150, HorizontalAlignment.Center)
            .Columns.Add("Address", 150, HorizontalAlignment.Center)
        End With
    End Sub

    Public Sub initChildren(ByVal listView As ListView)
        'this will add items to list view.
        With listView
            .Columns.Clear()
            .Items.Clear()
            listView.View = View.Details
            .Columns.Add("No", 33, HorizontalAlignment.Center)
            .Columns.Add("Name", 135, HorizontalAlignment.Center)
            .Columns.Add("Age", 30, HorizontalAlignment.Center)
            .Columns.Add("School", 135, HorizontalAlignment.Center)
        End With
    End Sub


    Public Sub initMobile(ByVal listView As ListView)
        'this will add items to list view.
        With listView
            .Columns.Clear()
            .Items.Clear()
            listView.View = View.Details
            .Columns.Add("No", 50, HorizontalAlignment.Center)
            .Columns.Add("Mobile No", 115, HorizontalAlignment.Center)
            .Columns.Add("Service", 115, HorizontalAlignment.Center)
        End With
    End Sub

    Public Sub setUserBuyer(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "children"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Children"
                End If
            Case "parents"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Parents"
                End If
            Case "siblings"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Siblings"
                End If
            Case "relatives"
                cmbName.SelectedIndex = 3
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Relatives"
                End If
            Case "other"
                cmbName.SelectedIndex = 4
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Other"
                End If
            Case "spouse"
                cmbName.SelectedIndex = 5
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Spouse"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "children"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Parents"
                End If
            Case "2"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Siblings"
                End If
            Case "3"
                cmbName.SelectedIndex = 3
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Relatives"
                End If
            Case "4"
                cmbName.SelectedIndex = 4
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Other"
                End If
            Case "5"
                cmbName.SelectedIndex = 5
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Spouse"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setPurpose(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "business"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Business"
                End If
            Case "personal service"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Personal Service"
                End If
            Case "raffle"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Raffle"
                End If
            Case "gift"
                cmbName.SelectedIndex = 3
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Gift"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Business"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Personal Service"
                End If
            Case "2"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Raffle"
                End If
            Case "3"
                cmbName.SelectedIndex = 3
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Business"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setUnitPayor(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "principal customer"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Principal Customer"
                End If
            Case "others"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Others"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Principal Customer"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Others"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setUnitPayr2(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "children"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Children"
                End If
            Case "parents"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Parents"
                End If
            Case "siblings"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Siblings"
                End If
            Case "relatives"
                cmbName.SelectedIndex = 3
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Relatives"
                End If
            Case "other"
                cmbName.SelectedIndex = 4
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Other"
                End If
            Case "spouse"
                cmbName.SelectedIndex = 5
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Spouse"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "children"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Parents"
                End If
            Case "2"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Siblings"
                End If
            Case "3"
                cmbName.SelectedIndex = 3
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Relatives"
                End If
            Case "4"
                cmbName.SelectedIndex = 4
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Other"
                End If
            Case "5"
                cmbName.SelectedIndex = 5
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Spouse"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setOwnership(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "owned"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Owned"
                End If
            Case "rented"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Rented"
                End If
            Case "caretaker"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Caretaker"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Owned"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Rented"
                End If
            Case "2"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Caretaker"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setOwnedOther(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "living with family"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Living With Family"
                End If
            Case "living with family (parents & siblings)"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Living With Family (Parents & Siblings)"
                End If
            Case "living with relatives"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Living With Relatives"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Living With Family"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Living With Family (Parents & Siblings)"
                End If
            Case "2"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Living With Relatives"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setGarage(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "no"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "No"
                End If
            Case "yes"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Yes"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "No"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Yes"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setHouseType(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "concrete"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Concrete"
                End If
            Case "concrete and wood"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Concrete and Wood"
                End If
            Case "wood"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Wood"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Concrete"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Concrete and Wood"
                End If
            Case "2"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Wood"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setIncomeSource(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "employed"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Employed"
                End If
            Case "self-employed"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Self-Employed"
                End If
            Case "with financer"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "With Financer"
                End If
            Case "pensioner"
                cmbName.SelectedIndex = 3
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Pensioner"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Employed"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Self-Employed"
                End If
            Case "2"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "With Financer"
                End If
            Case "3"
                cmbName.SelectedIndex = 3
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Pensioner"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setEmploymentSector(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "government"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Government"
                End If
            Case "private"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Private"
                End If
            Case "ofw"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "OFW"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Government"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Private"
                End If
            Case "2"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "OFW"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setEmpSector(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "public"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Public"
                End If
            Case "private"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Private"
                End If
            Case "self-employed"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Self-employed"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Public"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Private"
                End If
            Case "2"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Self-employed"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setIsUniformed(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "no"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "No"
                End If
            Case "yes"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Yes"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "No"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Yes"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setisIwithWheel(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "No"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Yes"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setWheelsOwnership(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "No"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Yes"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setWheelsTerm(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Cash"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Installment"
                End If
            Case "2"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Gift"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setWheelsActStat(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Fully Paid"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Existing"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setIsStudent(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "no"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "No"
                End If
            Case "yes"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Yes"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "No"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Yes"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setIsPrivate(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "no"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "No"
                End If
            Case "yes"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Yes"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "No"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Yes"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setIsMarried(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "no"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "No"
                End If
            Case "yes"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Yes"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "No"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Yes"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setIsChild(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "no"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "No"
                End If
            Case "yes"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Yes"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "No"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Yes"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setIsDependent(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "no"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "No"
                End If
            Case "yes"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Yes"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "No"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Yes"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setIsHousehold(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "no"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "No"
                End If
            Case "yes"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Yes"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "No"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Yes"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setIsScholar(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "no"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "No"
                End If
            Case "yes"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Yes"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "No"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Yes"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setHasWorked(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "no"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "No"
                End If
            Case "yes"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Yes"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "No"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Yes"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setIsMilitaryUniformed(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal loTxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "no"
                cmbName.SelectedIndex = 0
                If Not IsNothing(loTxt) Then
                    loTxt.Text = "No"
                End If
            Case "yes"
                cmbName.SelectedIndex = 1
                If Not IsNothing(loTxt) Then
                    loTxt.Text = "Yes"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(loTxt) Then
                    loTxt.Text = "No"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(loTxt) Then
                    loTxt.Text = "Yes"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(loTxt) Then
                    loTxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setAccountStatus(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "no"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "No"
                End If
            Case "yes"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Yes"
                End If
            Case "0"
                If Not IsNothing(lotxt) Then
                    cmbName.SelectedIndex = 0
                    lotxt.Text = "No"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Yes"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setGovernmentLevel(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "lgu"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "LGU"
                End If
            Case "provincial"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Provincial"
                End If
            Case "national"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "National"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "LGU"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Provincial"
                End If
            Case "2"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "National"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub
    Public Sub setCompanyLevel(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "local"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Local"
                End If
            Case "national"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "National"
                End If
            Case "multi-national"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Multi-National"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Local"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "National"
                End If
            Case "2"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Multi-National"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setEmploymentLevel(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "rank and file"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Rank and File"
                End If
            Case "supervisor"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Supervisor"
                End If
            Case "managerial"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Managerial"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Rank and File"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Supervisor"
                End If
            Case "2"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Managerial"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setOccptCateg(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "household services"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Household Services"
                End If
            Case "non-technical"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Non-Technical"
                End If
            Case "skilled/professional"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Skilled/Professional"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Household Services"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Non-Technical"
                End If
            Case "2"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Skilled/Professional"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setOFReg(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "america"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "America"
                End If
            Case "europe"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Europe"
                End If
            Case "ocenia"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Ocenia"
                End If
            Case "asia"
                cmbName.SelectedIndex = 3
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Asia"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "America"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Europe"
                End If
            Case "2"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Ocenia"
                End If
            Case "3"
                cmbName.SelectedIndex = 3
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Asia"
                End If
            Case Else
                cmbName.SelectedIndex = 4
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select

    End Sub

    Public Sub setCivilStat(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "single"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Single"
                End If
            Case "married"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Married"
                End If
            Case "separated"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Separated"
                End If
            Case "widowed"
                cmbName.SelectedIndex = 3
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Widowed"
                End If
            Case "single parent"
                cmbName.SelectedIndex = 4
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Single Parent"
                End If
            Case "single parent with live in partner"
                cmbName.SelectedIndex = 5
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Single Parent With Live In Partner"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Single"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Married"
                End If
            Case "2"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Separated"
                End If
            Case "3"
                cmbName.SelectedIndex = 3
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Widowed"
                End If
            Case "4"
                cmbName.SelectedIndex = 4
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Single Parent"
                End If
            Case "5"
                cmbName.SelectedIndex = 5
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Single Parent With Live In Partner"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setGender(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "male"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Male"
                End If
            Case "female"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Female"
                End If
            Case "lgbtq"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "LGBTQ"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Male"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Female"
                End If
            Case "2"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "LGBTQ"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setRent(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "living with family"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Living With Family"
                End If
            Case "living with family (parents & siblings)"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Living With Family (Parents & Siblings)"
                End If
            Case "living with relatives"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Living With Relatives"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Living With Family"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Living With Family (Parents & Siblings)"
                End If
            Case "2"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Living With Relatives"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setStatEmployment(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "regular"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Regular"
                End If
            Case "probationary"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Probationary"
                End If
            Case "contractual"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Contractual"
                End If
            Case "seasonal"
                cmbName.SelectedIndex = 3
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Seasonal"
                End If
            Case "r"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Regular"
                End If
            Case "p"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Probationary"
                End If
            Case "c"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Contractual"
                End If
            Case "s"
                cmbName.SelectedIndex = 3
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Seasonal"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Regular"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Probationary"
                End If
            Case "2"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Contractual"
                End If
            Case "3"
                cmbName.SelectedIndex = 3
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Seasonal"
                End If
        End Select
    End Sub

    Public Sub setBusinessOwnership(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "sole%proprietorship"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Sole Proprietorship"
                End If
            Case "partnership"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Partnership"
                End If
            Case "corporation"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Corporation"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Sole Proprietorship"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Partnership"
                End If
            Case "2"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Corporation"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setBusinessSize(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "Micro 1 (Less than 10,000 Income/Month)"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Micro 1 (Less than 10,000 Income/Month)"
                End If
            Case "Micro 2 (Less than 50,000 Income/Month)"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Micro 2 (Less than 50,000 Income/Month)"
                End If
            Case "Micro 3 (Less than 100,000 Income/Month)"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Micro 3 (Less than 100,000 Income/Month)"
                End If
            Case "Small (Less than 300,000 Income/Month)"
                cmbName.SelectedIndex = 3
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Small (Less than 300,000 Income/Month)"
                End If
            Case "Medium (Less than 1,000,000 Income/Month)"
                cmbName.SelectedIndex = 4
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Medium (Less than 1,000,000 Income/Month)"
                End If
            Case "Large (More than 1,000,000 Income/Month)"
                cmbName.SelectedIndex = 5
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Large (More than 1,000,000 Income/Month)"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Micro 1 (Less than 10,000 Income/Month)"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Micro 2 (Less than 50,000 Income/Month)"
                End If
            Case "2"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Micro 3 (Less than 100,000 Income/Month)"
                End If
            Case "3"
                cmbName.SelectedIndex = 3
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Small (Less than 300,000 Income/Month)"
                End If
            Case "4"
                cmbName.SelectedIndex = 4
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Medium (Less than 1,000,000 Income/Month)"
                End If
            Case "5"
                cmbName.SelectedIndex = 5
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Large (More than 1,000,000 Income/Month)"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setFinanceType(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "children"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Children"
                End If
            Case "parents"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Parents"
                End If
            Case "siblings"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Siblings"
                End If
            Case "relatives"
                cmbName.SelectedIndex = 3
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Relatives"
                End If
            Case "other"
                cmbName.SelectedIndex = 4
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Other"
                End If
            Case "spouse"
                cmbName.SelectedIndex = 5
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Spouse"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "children"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Parents"
                End If
            Case "2"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Siblings"
                End If
            Case "3"
                cmbName.SelectedIndex = 3
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Relatives"
                End If
            Case "4"
                cmbName.SelectedIndex = 4
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Other"
                End If
            Case "5"
                cmbName.SelectedIndex = 5
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Spouse"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setFinance(ByVal sValue As String, ByVal cmbName As ComboBox)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "children"
                cmbName.SelectedIndex = 0
            Case "parents"
                cmbName.SelectedIndex = 1
            Case "siblings"
                cmbName.SelectedIndex = 2
            Case "relatives"
                cmbName.SelectedIndex = 3
            Case "other"
                cmbName.SelectedIndex = 4
            Case "spouse"
                cmbName.SelectedIndex = 5
            Case "0"
                cmbName.SelectedIndex = 0
            Case "1"
                cmbName.SelectedIndex = 1
            Case "2"
                cmbName.SelectedIndex = 2
            Case "3"
                cmbName.SelectedIndex = 3
            Case "4"
                cmbName.SelectedIndex = 4
            Case "5"
                cmbName.SelectedIndex = 5
            Case Else
                cmbName.SelectedIndex = -1
        End Select
    End Sub

    Public Sub setPensionType(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "public"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Public"
                End If
            Case "private"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Private"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Public"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Private"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setApplicationType(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "motorcycle"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Motorcycle"
                End If
            Case "sidecar"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Sidecar"
                End If
            Case "others"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Others"
                End If
            Case "mobile phone"
                cmbName.SelectedIndex = 3
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Mobile Phone"
                End If
            Case "cars"
                cmbName.SelectedIndex = 4
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Cars"
                End If
            Case "services"
                cmbName.SelectedIndex = 5
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Services"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Motorcycle"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Sidecar"
                End If
            Case "2"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Others"
                End If
            Case "3"
                cmbName.SelectedIndex = 3
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Mobile Phone"
                End If
            Case "4"
                cmbName.SelectedIndex = 4
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Cars"
                End If
            Case "5"
                cmbName.SelectedIndex = 5
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Services"
                End If
            Case Else
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setTypeOfCustomer(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "new customer"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "New Customer"
                End If
            Case "repeat customer"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Repeat Customer"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "New Customer"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Repeat Customer"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setBankType(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "checking"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Checking"
                End If
            Case "savings"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Savings"
                End If
            Case "payroll"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Payroll"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Checking"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Savings"
                End If
            Case "2"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Payroll"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setRelationship(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "children"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Children"
                End If
            Case "parents"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Parents"
                End If
            Case "siblings"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Siblings"
                End If
            Case "relatives"
                cmbName.SelectedIndex = 3
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Relatives"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Children"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Parents"
                End If
            Case "2"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Siblings"
                End If
            Case "3"
                cmbName.SelectedIndex = 3
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Relatives"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setRel(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "children"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Children"
                End If
            Case "parents"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Parents"
                End If
            Case "siblings"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Siblings"
                End If
            Case "relatives"
                cmbName.SelectedIndex = 3
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Relatives"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Children"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Parents"
                End If
            Case "2"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Siblings"
                End If
            Case "3"
                cmbName.SelectedIndex = 3
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Relatives"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub


    Public Sub setUnitUser(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "principal customer"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Principal Customer"
                End If
            Case "others"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Others"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Principal Customer"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Others"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setEducationLevel(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "primary school"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Primary School"
                End If
            Case "secondary school"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Secondary School"
                End If
            Case "vocational/technical school"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Vocational/Technical School"
                End If
            Case "college"
                cmbName.SelectedIndex = 3
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "College"
                End If
            Case "graduate school"
                cmbName.SelectedIndex = 4
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Graduate School"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Primary School"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Secondary School"
                End If
            Case "2"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Vocational/Technical School"
                End If
            Case "3"
                cmbName.SelectedIndex = 3
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "College"
                End If
            Case "4"
                cmbName.SelectedIndex = 4
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Graduate School"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setEducLevel(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "primary school"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Primary School"
                End If
            Case "secondary school"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Secondary School"
                End If
            Case "vocational/technical school"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Vocational/Technical School"
                End If
            Case "college"
                cmbName.SelectedIndex = 3
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "College"
                End If
            Case "graduate school"
                cmbName.SelectedIndex = 4
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Graduate School"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Primary School"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Secondary School"
                End If
            Case "2"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Vocational/Technical School"
                End If
            Case "3"
                cmbName.SelectedIndex = 3
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "College"
                End If
            Case "4"
                cmbName.SelectedIndex = 4
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Graduate School"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub setEmploysector(ByVal sValue As String, ByVal cmbName As ComboBox, Optional ByVal lotxt As Label = Nothing)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "public"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Public"
                End If
            Case "private"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Private"
                End If
            Case "self-employed"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Self-Employed"
                End If
            Case "0"
                cmbName.SelectedIndex = 0
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Public"
                End If
            Case "1"
                cmbName.SelectedIndex = 1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Private"
                End If
            Case "2"
                cmbName.SelectedIndex = 2
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "Self-Employed"
                End If
            Case Else
                cmbName.SelectedIndex = -1
                If Not IsNothing(lotxt) Then
                    lotxt.Text = "N/A"
                End If
        End Select
    End Sub

    Public Sub initGrid(ByVal dgv As DataGridView)
        With dgv
            .Rows.Clear()
            .ColumnCount = 4
            .RowCount = 1
            .Columns(0).Name = "No"
            .Columns(1).Name = "Full Name"
            .Columns(2).Name = "Relationship"
            .Columns(3).Name = "Age"

            .Columns(0).Width = 50
            .Columns(1).Width = 280
            .Columns(2).Width = 250
            .Columns(3).Width = 60

            .Columns(0).Resizable = DataGridViewTriState.False
            .Columns(1).Resizable = DataGridViewTriState.False
            .Columns(2).Resizable = DataGridViewTriState.False
            .Columns(3).Resizable = DataGridViewTriState.False

            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            .Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft

        End With
    End Sub

    Public Sub initChild(ByVal dgv As DataGridView)
        With dgv
            .Rows.Clear()
            .ColumnCount = 3
            .RowCount = 1
            .Columns(0).Name = "No"
            .Columns(1).Name = "Full Name"
            .Columns(2).Name = "Age"

            .Columns(0).Width = 50
            .Columns(1).Width = 160
            .Columns(2).Width = 70

            .Columns(0).Resizable = DataGridViewTriState.False
            .Columns(1).Resizable = DataGridViewTriState.False
            .Columns(2).Resizable = DataGridViewTriState.False

            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            .Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft

        End With
    End Sub

    Public Sub initMobile(ByVal dgv As DataGridView)
        With dgv
            .Rows.Clear()
            .ColumnCount = 4
            .RowCount = 1
            .Columns(0).Name = "No"
            .Columns(1).Name = "Mobile"
            .Columns(2).Name = "Postpaid"
            .Columns(3).Name = "Year"

            .Columns(0).Width = 40
            .Columns(1).Width = 100
            .Columns(2).Width = 80
            .Columns(3).Width = 50

            .Columns(0).Resizable = DataGridViewTriState.False
            .Columns(1).Resizable = DataGridViewTriState.False
            .Columns(2).Resizable = DataGridViewTriState.False
            .Columns(3).Resizable = DataGridViewTriState.False

            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            .Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft

        End With
    End Sub

    Public Sub initSpouseMobile(ByVal dgv As DataGridView)
        With dgv
            .Rows.Clear()
            .ColumnCount = 3
            .RowCount = 1
            .Columns(0).Name = "No"
            .Columns(1).Name = "Mobile"
            .Columns(2).Name = "Postpaid"

            .Columns(0).Width = 40
            .Columns(1).Width = 165
            .Columns(2).Width = 65

            .Columns(0).Resizable = DataGridViewTriState.False
            .Columns(1).Resizable = DataGridViewTriState.False
            .Columns(2).Resizable = DataGridViewTriState.False

            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            .Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft

        End With
    End Sub


    Public Sub initLandline(ByVal dgv As DataGridView)
        With dgv
            .Rows.Clear()
            .ColumnCount = 2
            .RowCount = 1
            .Columns(0).Name = "No"
            .Columns(1).Name = "Landline"

            .Columns(0).Width = 40
            .Columns(1).Width = 230

            .Columns(0).Resizable = DataGridViewTriState.False
            .Columns(1).Resizable = DataGridViewTriState.False

            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            .Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft

        End With
    End Sub

    Public Sub initEmail(ByVal dgv As DataGridView)
        With dgv
            .Rows.Clear()
            .ColumnCount = 2
            .RowCount = 1
            .Columns(0).Name = "No"
            .Columns(1).Name = "Email Address"

            .Columns(0).Width = 40
            .Columns(1).Width = 230

            .Columns(0).Resizable = DataGridViewTriState.False
            .Columns(1).Resizable = DataGridViewTriState.False

            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            .Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft

        End With
    End Sub

    Public Sub initClientReference(ByVal dgv As DataGridView)
        With dgv
            .Rows.Clear()
            .ColumnCount = 3
            .RowCount = 1
            .Columns(0).Name = "No"
            .Columns(1).Name = "Client Name"
            .Columns(2).Name = "Address"

            .Columns(0).Width = 40
            .Columns(1).Width = 143
            .Columns(2).Width = 150

            .Columns(0).Resizable = DataGridViewTriState.False
            .Columns(1).Resizable = DataGridViewTriState.False
            .Columns(2).Resizable = DataGridViewTriState.False

            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            .Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft

        End With
    End Sub

    Public Sub initReference(ByVal dgv As DataGridView)
        With dgv
            .Rows.Clear()
            .ColumnCount = 5
            .RowCount = 1
            .Columns(0).Name = "No"
            .Columns(1).Name = "Full Name"
            .Columns(2).Name = "Contact No"
            .Columns(3).Name = "Address"
            .Columns(4).Name = "Town/City"

            .Columns(0).Width = 50
            .Columns(1).Width = 180
            .Columns(2).Width = 120
            .Columns(3).Width = 150
            .Columns(4).Width = 150

            .Columns(0).Resizable = DataGridViewTriState.False
            .Columns(1).Resizable = DataGridViewTriState.False
            .Columns(2).Resizable = DataGridViewTriState.False
            .Columns(3).Resizable = DataGridViewTriState.False
            .Columns(4).Resizable = DataGridViewTriState.False

            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            .Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft

        End With
    End Sub


    Public Sub initRefer(ByVal dgv As DataGridView)
        With dgv
            .Rows.Clear()
            .ColumnCount = 3
            .RowCount = 1
            .Columns(0).Name = "No"
            .Columns(1).Name = "Full Name"
            .Columns(2).Name = "Contact No"

            .Columns(0).Width = 50
            .Columns(1).Width = 140
            .Columns(2).Width = 100

            .Columns(0).Resizable = DataGridViewTriState.False
            .Columns(1).Resizable = DataGridViewTriState.False
            .Columns(2).Resizable = DataGridViewTriState.False

            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            .Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
        End With
    End Sub

    Public Sub initChildren(ByVal dgv As DataGridView)
        With dgv
            .Rows.Clear()
            .ColumnCount = 4
            .RowCount = 1
            .Columns(0).Name = "No."
            .Columns(1).Name = "Name"
            .Columns(2).Name = "Age"
            .Columns(3).Name = "School/ Company"

            .Columns(0).Width = 40
            .Columns(1).Width = 120
            .Columns(2).Width = 70
            .Columns(3).Width = 105

            .Columns(0).Resizable = DataGridViewTriState.False
            .Columns(1).Resizable = DataGridViewTriState.False
            .Columns(2).Resizable = DataGridViewTriState.False
            .Columns(3).Resizable = DataGridViewTriState.False

            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            .Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
        End With
    End Sub

    Public Sub initNumber(ByVal dgv As DataGridView)
        With dgv
            .Rows.Clear()
            .ColumnCount = 3
            .RowCount = 1
            .Columns(0).Name = "No."
            .Columns(1).Name = "Mobile No"
            .Columns(2).Name = "Service"

            .Columns(0).Width = 50
            .Columns(1).Width = 110
            .Columns(2).Width = 140

            .Columns(0).Resizable = DataGridViewTriState.False
            .Columns(1).Resizable = DataGridViewTriState.False
            .Columns(2).Resizable = DataGridViewTriState.False

            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            .Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
        End With
    End Sub

    Public Sub initSMobile(ByVal dgv As DataGridView)
        With dgv
            .Rows.Clear()
            .ColumnCount = 2
            .RowCount = 1
            .Columns(0).Name = "No."
            .Columns(1).Name = "Mobile No"

            .Columns(0).Width = 70
            .Columns(1).Width = 250

            .Columns(0).Resizable = DataGridViewTriState.False
            .Columns(1).Resizable = DataGridViewTriState.False

            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            .Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
        End With
    End Sub


    Public Sub setRelation(ByVal sValue As String, ByVal cmbName As ComboBox)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "children"
                cmbName.SelectedIndex = 0
            Case "parents"
                cmbName.SelectedIndex = 1
            Case "siblings"
                cmbName.SelectedIndex = 2
            Case "relatives"
                cmbName.SelectedIndex = 3
            Case "0"
                cmbName.SelectedIndex = 0
            Case "1"
                cmbName.SelectedIndex = 1
            Case "2"
                cmbName.SelectedIndex = 2
            Case "3"
                cmbName.SelectedIndex = 3
            Case Else
                cmbName.SelectedIndex = -1
        End Select
    End Sub

    Public Sub setEmploymn(ByVal sValue As String, ByVal cmbName As ComboBox)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "regular"
                cmbName.SelectedIndex = 0
            Case "probationary"
                cmbName.SelectedIndex = 1
            Case "contractual"
                cmbName.SelectedIndex = 2
            Case "seasonal"
                cmbName.SelectedIndex = 3
            Case "r"
                cmbName.SelectedIndex = 0
            Case "p"
                cmbName.SelectedIndex = 1
            Case "c"
                cmbName.SelectedIndex = 2
            Case "s"
                cmbName.SelectedIndex = 3
            Case Else
                cmbName.SelectedIndex = -1
        End Select
    End Sub

    Public Sub setCustomerType(ByVal sValue As String, ByVal cmbName As ComboBox)
        If IsNothing(sValue) Then
            sValue = ""
        End If
        Select Case sValue.ToLower
            Case "new customer"
                cmbName.SelectedIndex = 0
            Case "repeat customer"
                cmbName.SelectedIndex = 1
            Case "0"
                cmbName.SelectedIndex = 0
            Case "1"
                cmbName.SelectedIndex = 1
            Case Else
                cmbName.SelectedIndex = -1
        End Select
    End Sub
End Module
