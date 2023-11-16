Imports Terminal.Gui
Imports VSData

Friend Class MainWindow
    Inherits Toplevel
    Private ReadOnly store As DataStore
    Private ReadOnly categoriesMenuBarItem As MenuBarItem
    Private ReadOnly panelView As PanelView
    Private ReadOnly mediaTypesMenuBarItem As MenuBarItem
    Sub New(store As DataStore)
        Me.store = store
        categoriesMenuBarItem = New MenuBarItem() With
                    {
                        .Title = "Categories"
                    }
        mediaTypesMenuBarItem = New MenuBarItem() With
                    {
                        .Title = "Media Types"
                    }
        UpdateCategoryMenu()
        UpdateMediaTypesMenu()

        Dim menuBar = New MenuBar With
            {
                .Menus = New MenuBarItem() {
                    categoriesMenuBarItem,
                    mediaTypesMenuBarItem
                }
            }
        panelView = New PanelView With
            {
                .Text = "I am a panel",
                .Y = 1,
                .Width = [Dim].Fill,
                .Height = [Dim].Fill - 1
            }
        Add(menuBar, panelView)
    End Sub

    Private Sub UpdateMediaTypesMenu()
        Dim children As New List(Of MenuItem)
        For Each item In store.MediaTypeList
            children.Add(New MenuItem() With {
                .Title = $"{item.Abbr} - {item.Name}",
                .Action = Sub()
                              ShowMediaType(item.Id)
                          End Sub
            })
        Next
        mediaTypesMenuBarItem.Children = children.ToArray
    End Sub

    Private Sub ShowMediaType(mediaTypeId As Integer)
        panelView.RemoveAll()
        Dim listView As New ListView(store.MediaTypeMediaList(mediaTypeId).ToList) With
            {
                .Width = [Dim].Fill,
                .Height = [Dim].Fill
            }
        panelView.Add(listView)
    End Sub

    Private Sub UpdateCategoryMenu()
        Dim children As New List(Of MenuItem)
        For Each item In store.CategoryList
            children.Add(New MenuItem() With {
                .Title = $"{item.Abbr} - {item.Name}",
                .Action = Sub()
                              ShowCategory(item.Id)
                          End Sub
            })
        Next
        categoriesMenuBarItem.Children = children.ToArray
    End Sub

    Private Sub ShowCategory(categoryId As Integer)
        Throw New NotImplementedException()
    End Sub
End Class
