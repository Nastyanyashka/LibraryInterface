﻿using System.ComponentModel;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using Library;
using Library.Entities;
using LibraryInterface.Classes;
using Microsoft.EntityFrameworkCore;
namespace LibraryInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext dbForUser = new ApplicationContext();
        public List<Library.Entities.UserInfo> userInfo = new List<Library.Entities.UserInfo>();
        public ApplicationContext DbForUser { get { return dbForUser; } set { dbForUser = value; } }
        ApplicationContext db = new ApplicationContext();

        List<Library.Entities.MenuInfo> menuInfos = new List<Library.Entities.MenuInfo>();

        bool strangeEvent = false;
        bool edit = false;
        bool editCurrentMenu = false;
        bool antiLoop = false;
        public MainWindow()
        {
            InitializeComponent();

            menuInfos = db.MenuInfo.OrderBy(o => o.ParentId).ToList();
            List<MenuItem> mainMenuItems = new List<MenuItem>();
            List<MenuInfo> mainMenuInfos = new List<MenuInfo>();
            for (int i = 0; i < menuInfos.Count; i++)
            {
                MyMenuItem item = new MyMenuItem();
                item.Id = menuInfos[i].Id;
                for (int k = 0; k < userInfo.Count; k++)
                {
                    if (item.Id == userInfo[k].MenuInfoId)
                    {
                        if (userInfo[k].Read == false)
                        {
                            item.Visibility = Visibility.Collapsed;
                            break;
                        }
                    }
                }

                item.Header = menuInfos[i].Title;
                RoutedEventHandler? handler = null;
                if (menuInfos[i].NameOfFunc != "")
                {
                    handler = (RoutedEventHandler)Delegate.CreateDelegate(typeof(RoutedEventHandler), this, menuInfos[i].NameOfFunc);
                    item.Click += handler;
                }
                if (menuInfos[i].ParentId == 0)
                {
                    mainMenuInfos.Add(menuInfos[i]);
                    mainMenuItems.Add(item);
                    continue;
                }

                for (int j = 0; j < mainMenuItems.Count; j++)
                {
                    if (mainMenuInfos[j].Id == menuInfos[i].ParentId)
                    {
                        mainMenuItems[j].Items.Add(item);
                    }
                }
            }
            for (int i = 0; i < mainMenuItems.Count; i++)
            {
                menu.Items.Add(mainMenuItems[i]);
            }



            firstGrid.InitializingNewItem += FirstGrid_InitializingNewItem;
            firstGrid.PreviewKeyDown += FirstGrid_PreviewKeyDown;
            firstGrid.CanUserAddRows = false;
            firstGrid.CanUserDeleteRows = false;
            firstGrid.IsReadOnly = true;
            firstGrid.IsLocked = false;

        }



        private void FirstGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Delete == e.Key)
            {
                var grid = (DataGrid)sender;
                if (grid.SelectedItems.Count > 0)
                {
                    foreach (var row in grid.SelectedItems)
                    {
                        if (row is UserInfo)
                        {
                            db.UserInfo.Remove((UserInfo)row);
                            //db.SaveChanges();
                            return;
                        }
                        if (row is CompositionsAndPublishers)
                        {
                            db.CompositionsAndPublishers.Remove((CompositionsAndPublishers)row);
                            return;
                        }
                        if (row is GivenExamplers)
                        {
                            db.GivenExamplers.Remove((GivenExamplers)row);
                            return;
                        }
                        if (row is AuthorsAndCompositions)
                        {
                            db.AuthorsAndCompositions.Remove((AuthorsAndCompositions)row);
                            return;
                        }
                        if (row is ReadersAndPenaltys)
                        {
                            db.ReadersAndPenaltys.Remove((ReadersAndPenaltys)row);
                            return;
                        }
                        db.Remove(row);
                    }

                }
            }
        }

        private void FirstGrid_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            if (e.NewItem is UserInfo)
            {
                db.UserInfo.Add((UserInfo)e.NewItem);
                return;
            }
            if (e.NewItem is CompositionsAndPublishers)
            {
                db.CompositionsAndPublishers.Add((CompositionsAndPublishers)e.NewItem);
                return;
            }
            if (e.NewItem is GivenExamplers)
            {
                db.GivenExamplers.Add((GivenExamplers)e.NewItem);
                return;
            }
            if (e.NewItem is AuthorsAndCompositions)
            {
                db.AuthorsAndCompositions.Add((AuthorsAndCompositions)e.NewItem);
                return;
            }
            if (e.NewItem is ReadersAndPenaltys)
            {
                db.ReadersAndPenaltys.Add((ReadersAndPenaltys)e.NewItem);
                return;
            }
            db.Add(e.NewItem);
            //db.SaveChanges();
            //call '.Reference().TargetEntry' or '.Collection().FindEntry()' on the owner entry.'

        }

        private void DataGridChanged(object sender, SelectionChangedEventArgs e)
        {
            var source = e.RemovedItems;
            if (source.Count > 0 && antiLoop == false)
            {
                if (firstGrid.IsLocked == false && e.RemovedItems[0] is DateTime)
                {

                    antiLoop = true;
                    var datePicker = sender as DatePicker;
                    datePicker!.SelectedDate = (DateTime)source[0]!;

                }
                antiLoop = false;
            }

        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void Composition_Click(object sender, RoutedEventArgs e)
        {
            firstGrid.Visibility = Visibility.Hidden;
            firstGrid.ItemsSource = null;
            firstGrid.Columns.Clear();
            if (strangeEvent == true)
            {
                firstGrid.RowEditEnding -= FirstGrid_SourceUpdated;
                strangeEvent = false;
            }
            UserInfo info = userInfo.Find(x => x.MenuInfoId == (sender as MyMenuItem)!.Id)!;

            Granting_Rights(info);

            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Название";
            col1.Binding = new Binding("Name");
            firstGrid.Columns.Add(col1);


            DataGridNumericColumn col2 = new DataGridNumericColumn();
            col2.Header = "Кол-во в библиотеке";
            col2.Binding = new Binding("AmountInLibrary");
            firstGrid.Columns.Add(col2);


            DataGridNumericColumn col3 = new DataGridNumericColumn();
            col3.Header = "Год";
            col3.Binding = new Binding("Year");
            firstGrid.Columns.Add(col3);


            DataGridComboBoxColumn comboColumn2 = new DataGridComboBoxColumn();
            comboColumn2.Header = "Тип";
            comboColumn2.ItemsSource = db.TypesOfComposition.ToList();
            comboColumn2.SelectedValueBinding = new Binding("TypeId");
            comboColumn2.SelectedValuePath = "Id";
            comboColumn2.DisplayMemberPath = "Name";
            firstGrid.Columns.Add(comboColumn2);

            var comboBoxColumn = new DataGridTemplateColumn();
            comboBoxColumn.Header = "Авторы";
            FrameworkElementFactory comboBoxFactory = new FrameworkElementFactory(typeof(ButttonWInfo));
            comboBoxFactory.AddHandler(ButttonWInfo.ClickEvent, new RoutedEventHandler(ButtonInfo_Click2));
            comboBoxFactory.SetValue(ButttonWInfo.ContentProperty, "Открыть");
            DataTemplate cellTemplate = new DataTemplate();
            cellTemplate.VisualTree = comboBoxFactory;
            comboBoxColumn.CellTemplate = cellTemplate;
            firstGrid.Columns.Add(comboBoxColumn);

            var buttonColumn = new DataGridTemplateColumn();
            buttonColumn.Header = "Экземпляры";
            FrameworkElementFactory buttonFactory = new FrameworkElementFactory(typeof(ButttonWInfo));
            buttonFactory.AddHandler(ButttonWInfo.ClickEvent, new RoutedEventHandler(ButtonInfo_Click));
            buttonFactory.SetValue(ButttonWInfo.ContentProperty, "Открыть");
            DataTemplate cellTemplate2 = new DataTemplate();
            cellTemplate2.VisualTree = buttonFactory;
            buttonColumn.CellTemplate = cellTemplate2;
            firstGrid.Columns.Add(buttonColumn);

            var compositions = db.Compositions.Include(e => e.Examplers).ToList();
            firstGrid.ItemsSource = compositions;


            for (int i = 0; i < compositions.Count; i++)
            {
                firstGrid.UpdateLayout();
                firstGrid.ScrollIntoView(firstGrid.Items[i]);
                DataGridRow dataGridRow = (DataGridRow)firstGrid.ItemContainerGenerator.ContainerFromIndex(i);
                List<ButttonWInfo> comboBoxes = FindVisualChildren<ButttonWInfo>(dataGridRow);
                for (int j = 0; j < comboBoxes.Count; j++)
                {
                    comboBoxes[j].SomeId = compositions[i].Id;
                }
            }



            firstGrid.Visibility = Visibility.Visible;

        }


        private void ButtonInfo_Click2(object sender, RoutedEventArgs e)
        {
            var button = sender as ButttonWInfo;


            MainWindow examplersWindow = new MainWindow();
            examplersWindow.Show();
            examplersWindow.menu.Visibility = Visibility.Collapsed;


            examplersWindow.firstGrid.Visibility = Visibility.Hidden;
            examplersWindow.firstGrid.ItemsSource = null;
            examplersWindow.firstGrid.Columns.Clear();
            if (strangeEvent == true)
            {
                examplersWindow.firstGrid.RowEditEnding -= FirstGrid_SourceUpdated;
                strangeEvent = false;
            }

            UserInfo info = userInfo.Find(x => x.MenuInfo!.Title == "Произведения")!;
            examplersWindow.Granting_Rights(info);

            List<Author> authors = db.Authors.ToList();
            List<ReaderFullName> readerFullNames = new List<ReaderFullName>();
            for (int i = 0; i < authors.Count; i++)
            {
                readerFullNames.Add(new ReaderFullName(authors[i].FirstName,
                    authors[i].LastName, authors[i].MiddleName)
                { Id = authors[i].Id });
            }
            DataGridComboBoxColumn comboColumn3 = new DataGridComboBoxColumn();
            comboColumn3.Header = "Автор";
            comboColumn3.ItemsSource = readerFullNames;
            comboColumn3.SelectedValueBinding = new Binding("AuthorId");
            comboColumn3.SelectedValuePath = "Id";
            comboColumn3.DisplayMemberPath = "FullName";
            examplersWindow.firstGrid.Columns.Add(comboColumn3);


            var examplers = db.AuthorsAndCompositions.Where(e => e.CompositionId == button!.SomeId).ToList();


            examplersWindow.firstGrid.RowEditEnding += (sender, e) =>
            {
                var item = e.Row.Item as AuthorsAndCompositions;
                item.CompositionId = button!.SomeId;
                examplersWindow.db.AuthorsAndCompositions.Add(item);
            };
            examplersWindow.firstGrid.ItemsSource = examplers;


            examplersWindow.firstGrid.Visibility = Visibility.Visible;
        }

        private void ButtonInfo_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as ButttonWInfo;


            MainWindow examplersWindow = new MainWindow();
            examplersWindow.Show();
            examplersWindow.menu.Visibility = Visibility.Collapsed;


            examplersWindow.firstGrid.Visibility = Visibility.Hidden;
            examplersWindow.firstGrid.ItemsSource = null;
            examplersWindow.firstGrid.Columns.Clear();
            if (strangeEvent == true)
            {
                firstGrid.RowEditEnding -= FirstGrid_SourceUpdated;
                strangeEvent = false;
            }


            UserInfo info = userInfo.Find(x => x.MenuInfo!.Title == "Произведения")!;
            examplersWindow.Granting_Rights(info);


            DataGridNumericColumn col5 = new DataGridNumericColumn();
            col5.Header = "Инвентарный номер";
            col5.Binding = new Binding("Id");
            examplersWindow.firstGrid.Columns.Add(col5);


            DataGridComboBoxColumn comboColumn2 = new DataGridComboBoxColumn();
            comboColumn2.Header = "Хранилище";
            comboColumn2.ItemsSource = db.Storages.ToList();
            comboColumn2.SelectedValueBinding = new Binding("StorageId");
            comboColumn2.SelectedValuePath = "Id";
            comboColumn2.DisplayMemberPath = "Type";
            examplersWindow.firstGrid.Columns.Add(comboColumn2);


            DataGridNumericColumn col1 = new DataGridNumericColumn();
            col1.Header = "Номер стеллажа";
            col1.Binding = new Binding("NumberOfRack");
            examplersWindow.firstGrid.Columns.Add(col1);

            DataGridNumericColumn col2 = new DataGridNumericColumn();
            col2.Header = "Номер полки";
            col2.Binding = new Binding("NumberOfShelf");
            examplersWindow.firstGrid.Columns.Add(col2);



            var examplers = db.Examplers.Where(e => e.CompositionId == button!.SomeId).ToList();

            examplersWindow.firstGrid.RowEditEnding += (sender, e) =>
            {
                var item = e.Row.Item as Exampler;
                item.CompositionId = button!.SomeId;
                examplersWindow.db.Add(e.Row.Item);
            };
            examplersWindow.firstGrid.ItemsSource = examplers;


            examplersWindow.firstGrid.Visibility = Visibility.Visible;
        }

        private List<T> FindVisualChildren<T>(DependencyObject parent) where T : DependencyObject
        {
            List<T> foundChildren = new List<T>();

            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child != null && child is T)
                {
                    foundChildren.Add((T)child);
                }
                else
                {
                    List<T> childrenOfChild = FindVisualChildren<T>(child);
                    if (childrenOfChild != null)
                    {
                        foundChildren.AddRange(childrenOfChild);
                    }
                }
            }

            return foundChildren;
        }


        private void CompositionsAndPublishers_Click(object sender, RoutedEventArgs e)
        {
            firstGrid.Visibility = Visibility.Hidden;
            firstGrid.ItemsSource = null;
            firstGrid.Columns.Clear();
            if (strangeEvent == true)
            {
                firstGrid.RowEditEnding -= FirstGrid_SourceUpdated;
                strangeEvent = false;
            }
            UserInfo info = userInfo.Find(x => x.MenuInfoId == (sender as MyMenuItem)!.Id)!;

            Granting_Rights(info);

            DataGridComboBoxColumn comboColumn2 = new DataGridComboBoxColumn();
            comboColumn2.Header = "Произведение";
            comboColumn2.ItemsSource = db.Compositions.ToList();
            comboColumn2.SelectedValueBinding = new Binding("CompostionId");
            comboColumn2.SelectedValuePath = "Id";
            comboColumn2.DisplayMemberPath = "Name";
            firstGrid.Columns.Add(comboColumn2);

            DataGridComboBoxColumn comboColumn3 = new DataGridComboBoxColumn();
            comboColumn3.Header = "Издательство";
            comboColumn3.ItemsSource = db.PlacesOfPublications.ToList();
            comboColumn3.SelectedValueBinding = new Binding("PlaceOfPublicationId");
            comboColumn3.SelectedValuePath = "Id";
            comboColumn3.DisplayMemberPath = "Name";
            firstGrid.Columns.Add(comboColumn3);

            firstGrid.ItemsSource = db.CompositionsAndPublishers.ToList();
            firstGrid.Visibility = Visibility.Visible;
        }
        private void Exampler_Click(object sender, RoutedEventArgs e)
        {
            firstGrid.Visibility = Visibility.Hidden;
            firstGrid.ItemsSource = null;
            firstGrid.Columns.Clear();
            if (strangeEvent == true)
            {
                firstGrid.RowEditEnding -= FirstGrid_SourceUpdated;
                strangeEvent = false;
            }
            UserInfo info = userInfo.Find(x => x.MenuInfoId == (sender as MyMenuItem)!.Id)!;
            Granting_Rights(info);

            DataGridComboBoxColumn comboColumn3 = new DataGridComboBoxColumn();
            comboColumn3.Header = "Произведение";
            comboColumn3.ItemsSource = db.Compositions.ToList();
            comboColumn3.SelectedValueBinding = new Binding("CompositionId");
            comboColumn3.SelectedValuePath = "Id";
            comboColumn3.DisplayMemberPath = "Name";
            firstGrid.Columns.Add(comboColumn3);

            DataGridComboBoxColumn comboColumn2 = new DataGridComboBoxColumn();
            comboColumn2.Header = "Хранилище";
            comboColumn2.ItemsSource = db.Storages.ToList();
            comboColumn2.SelectedValueBinding = new Binding("StorageId");
            comboColumn2.SelectedValuePath = "Id";
            comboColumn2.DisplayMemberPath = "Type";
            firstGrid.Columns.Add(comboColumn2);


            DataGridNumericColumn col1 = new DataGridNumericColumn();
            col1.Header = "Номер стеллажа";
            col1.Binding = new Binding("NumberOfRack");
            firstGrid.Columns.Add(col1);

            DataGridNumericColumn col2 = new DataGridNumericColumn();
            col2.Header = "Номер полки";
            col2.Binding = new Binding("NumberOfShelf");
            firstGrid.Columns.Add(col2);




            firstGrid.ItemsSource = db.Examplers.ToList();
            firstGrid.Visibility = Visibility.Visible;
        }
       

        private void InterlibrarySubscription_Click(object sender, RoutedEventArgs e)
        {
            firstGrid.Visibility = Visibility.Hidden;
            firstGrid.ItemsSource = null;
            firstGrid.Columns.Clear();
            if (strangeEvent == true)
            {
                firstGrid.RowEditEnding -= FirstGrid_SourceUpdated;
                strangeEvent = false;
            }
            UserInfo info = userInfo.Find(x => x.MenuInfoId == (sender as MyMenuItem)!.Id)!;
            Granting_Rights(info);

            List<Reader> readers = db.Readers.ToList();
            List<ReaderFullName> readerFullNames = new List<ReaderFullName>();
            for (int i = 0; i < readers.Count; i++)
            {
                readerFullNames.Add(new ReaderFullName(readers[i].FirstName,
                    readers[i].LastName, readers[i].MiddleName)
                { Id = readers[i].Id });
            }
            DataGridComboBoxColumn comboColumn2 = new DataGridComboBoxColumn();
            comboColumn2.Header = "Читатель";
            comboColumn2.ItemsSource = readerFullNames;
            comboColumn2.SelectedValueBinding = new Binding("ReaderId");
            comboColumn2.SelectedValuePath = "Id";
            comboColumn2.DisplayMemberPath = "FullName";
            firstGrid.Columns.Add(comboColumn2);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Header = "Книга";
            col2.Binding = new Binding("Name");
            firstGrid.Columns.Add(col2);

            DataGridTextColumn col5 = new DataGridTextColumn();
            col5.Header = "Библиотека";
            col5.Binding = new Binding("NameOfLibrary");
            firstGrid.Columns.Add(col5);


            DataGridTemplateColumn templateColumn = new DataGridTemplateColumn();
            DataTemplate dataTemplate = new DataTemplate();
            FrameworkElementFactory datePicker = new FrameworkElementFactory(typeof(DatePicker));
            datePicker.SetBinding(DatePicker.SelectedDateProperty, new Binding("DateOfOrder") { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
            datePicker.AddHandler(DatePicker.SelectedDateChangedEvent, new EventHandler<SelectionChangedEventArgs>(DataGridChanged!));
            dataTemplate.VisualTree = datePicker;
            templateColumn.Header = "День заказа";
            templateColumn.CellTemplate = dataTemplate;
            firstGrid.Columns.Add(templateColumn);

            DataGridTemplateColumn templateColumn2 = new DataGridTemplateColumn();
            DataTemplate dataTemplate2 = new DataTemplate();
            FrameworkElementFactory datePicker2 = new FrameworkElementFactory(typeof(DatePicker));
            datePicker2.SetBinding(DatePicker.SelectedDateProperty, new Binding("ArrivalDate") { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
            datePicker2.AddHandler(DatePicker.SelectedDateChangedEvent, new EventHandler<SelectionChangedEventArgs>(DataGridChanged!));
            dataTemplate2.VisualTree = datePicker2;
            templateColumn2.Header = "День прибытия";
            templateColumn2.CellTemplate = dataTemplate2;
            firstGrid.Columns.Add(templateColumn2);



            firstGrid.ItemsSource = db.InterlibrarySubscriptions.ToList();
            firstGrid.Visibility = Visibility.Visible;
        }
        private void Penalty_Click(object sender, RoutedEventArgs e)
        {
            firstGrid.Visibility = Visibility.Hidden;
            firstGrid.ItemsSource = null;
            firstGrid.Columns.Clear();
            if (strangeEvent == true)
            {
                firstGrid.RowEditEnding -= FirstGrid_SourceUpdated;
                strangeEvent = false;
            }
            UserInfo info = userInfo.Find(x => x.MenuInfoId == (sender as MyMenuItem)!.Id)!;
            Granting_Rights(info);


            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Название";
            col1.Binding = new Binding("Name");
            firstGrid.Columns.Add(col1);

            DataGridNumericColumn col2 = new DataGridNumericColumn();
            col2.Header = "Дней отстранения";
            col2.Binding = new Binding("NumOfSuspensionDays");
            firstGrid.Columns.Add(col2);

            DataGridNumericColumn col3 = new DataGridNumericColumn();
            col3.Header = "Сумма выплаты";
            col3.Binding = new Binding("Sum");
            firstGrid.Columns.Add(col3);

            firstGrid.ItemsSource = db.Penalties.ToList();
            firstGrid.Visibility = Visibility.Visible;
        }
        private void PlaceOfPublication_Click(object sender, RoutedEventArgs e)
        {
            firstGrid.Visibility = Visibility.Hidden;
            firstGrid.ItemsSource = null;
            firstGrid.Columns.Clear();
            if (strangeEvent == true)
            {
                firstGrid.RowEditEnding -= FirstGrid_SourceUpdated;
                strangeEvent = false;
            }
            UserInfo info = userInfo.Find(x => x.MenuInfoId == (sender as MyMenuItem)!.Id)!;
            Granting_Rights(info);

            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Название";
            col1.Binding = new Binding("Name");
            firstGrid.Columns.Add(col1);

            DataGridComboBoxColumn comboColumn2 = new DataGridComboBoxColumn();
            comboColumn2.Header = "Город";
            comboColumn2.ItemsSource = db.Cities.ToList();
            comboColumn2.SelectedValueBinding = new Binding("CityId");
            comboColumn2.SelectedValuePath = "Id";
            comboColumn2.DisplayMemberPath = "Name";
            firstGrid.Columns.Add(comboColumn2);

            firstGrid.ItemsSource = db.PlacesOfPublications.ToList();
            firstGrid.Visibility = Visibility.Visible;
        }

        private void Reader_Click(object sender, RoutedEventArgs e)
        {
            firstGrid.Visibility = Visibility.Hidden;
            firstGrid.ItemsSource = null;
            firstGrid.Columns.Clear();
            if (strangeEvent == true)
            {
                firstGrid.RowEditEnding -= FirstGrid_SourceUpdated;
                strangeEvent = false;
            }
            UserInfo info = userInfo.Find(x => x.MenuInfoId == (sender as MyMenuItem)!.Id)!;
            Granting_Rights(info);

            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Фамилия";
            col1.Binding = new Binding("LastName");
            firstGrid.Columns.Add(col1);


            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Header = "Имя";
            col2.Binding = new Binding("FirstName");
            firstGrid.Columns.Add(col2);


            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Header = "Отчество";
            col3.Binding = new Binding("MiddleName");
            firstGrid.Columns.Add(col3);

            DataGridTextColumn col5 = new DataGridTextColumn();
            col5.Header = "Читательский билет";
            col5.Binding = new Binding("ReaderTicket");
            firstGrid.Columns.Add(col5);

            DataGridTemplateColumn templateColumn = new DataGridTemplateColumn();
            DataTemplate dataTemplate = new DataTemplate();
            FrameworkElementFactory datePicker = new FrameworkElementFactory(typeof(DatePicker));
            datePicker.SetBinding(DatePicker.SelectedDateProperty, new Binding("RegistrationDate") { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
            datePicker.AddHandler(DatePicker.SelectedDateChangedEvent, new EventHandler<SelectionChangedEventArgs>(DataGridChanged!));
            dataTemplate.VisualTree = datePicker;
            templateColumn.Header = "Дата регистраци";
            templateColumn.CellTemplate = dataTemplate;
            firstGrid.Columns.Add(templateColumn);

            DataGridTemplateColumn templateColumn2 = new DataGridTemplateColumn();
            DataTemplate dataTemplate2 = new DataTemplate();
            FrameworkElementFactory datePicker2 = new FrameworkElementFactory(typeof(DatePicker));
            datePicker2.SetBinding(DatePicker.SelectedDateProperty, new Binding("PereregistrationDate") { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
            datePicker2.AddHandler(DatePicker.SelectedDateChangedEvent, new EventHandler<SelectionChangedEventArgs>(DataGridChanged!));
            dataTemplate2.VisualTree = datePicker2;
            templateColumn2.Header = "День перерегистрации";
            templateColumn2.CellTemplate = dataTemplate2;
            firstGrid.Columns.Add(templateColumn2);


            DataGridComboBoxColumn comboColumn2 = new DataGridComboBoxColumn();
            comboColumn2.Header = "Факультет";
            comboColumn2.ItemsSource = db.Faculties.ToList();
            comboColumn2.SelectedValueBinding = new Binding("FacultyId");
            comboColumn2.SelectedValuePath = "Id";
            comboColumn2.DisplayMemberPath = "Name";
            firstGrid.Columns.Add(comboColumn2);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Header = "Группа";
            col4.Binding = new Binding("Group");
            firstGrid.Columns.Add(col4);


            DataGridComboBoxColumn comboColumn6 = new DataGridComboBoxColumn();
            comboColumn6.Header = "Степень";
            comboColumn6.ItemsSource = db.Degrees.ToList();
            comboColumn6.SelectedValueBinding = new Binding("DegreeId");
            comboColumn6.SelectedValuePath = "Id";
            comboColumn6.DisplayMemberPath = "Name";
            firstGrid.Columns.Add(comboColumn6);

            DataGridComboBoxColumn comboColumn3 = new DataGridComboBoxColumn();
            comboColumn3.Header = "Звание";
            comboColumn3.ItemsSource = db.Ranks.ToList();
            comboColumn3.SelectedValueBinding = new Binding("RankId");
            comboColumn3.SelectedValuePath = "Id";
            comboColumn3.DisplayMemberPath = "Name";
            firstGrid.Columns.Add(comboColumn3);


            DataGridComboBoxColumn comboColumn4 = new DataGridComboBoxColumn();
            comboColumn4.Header = "Кафедра";
            comboColumn4.ItemsSource = db.Departments.ToList();
            comboColumn4.SelectedValueBinding = new Binding("DepartmentId");
            comboColumn4.SelectedValuePath = "Id";
            comboColumn4.DisplayMemberPath = "Name";
            firstGrid.Columns.Add(comboColumn4);


            DataGridComboBoxColumn comboColumn5 = new DataGridComboBoxColumn();
            comboColumn5.Header = "Должность";
            comboColumn5.ItemsSource = db.Positions.ToList();
            comboColumn5.SelectedValueBinding = new Binding("PositionId");
            comboColumn5.SelectedValuePath = "Id";
            comboColumn5.DisplayMemberPath = "Name";
            firstGrid.Columns.Add(comboColumn5);



            var comboBoxColumn = new DataGridTemplateColumn();
            comboBoxColumn.Header = "Выданные экземпляры";
            FrameworkElementFactory comboBoxFactory = new FrameworkElementFactory(typeof(ButttonWInfo));
            comboBoxFactory.AddHandler(ButttonWInfo.ClickEvent, new RoutedEventHandler(ButtonInfo_Click3));
            comboBoxFactory.SetValue(ButttonWInfo.ContentProperty, "Открыть");
            DataTemplate cellTemplate = new DataTemplate();
            cellTemplate.VisualTree = comboBoxFactory;
            comboBoxColumn.CellTemplate = cellTemplate;
            firstGrid.Columns.Add(comboBoxColumn);

            var buttonColumn = new DataGridTemplateColumn();
            buttonColumn.Header = "Штрафы";
            FrameworkElementFactory buttonFactory = new FrameworkElementFactory(typeof(ButttonWInfo));
            buttonFactory.AddHandler(ButttonWInfo.ClickEvent, new RoutedEventHandler(ButtonInfo_Click4));
            buttonFactory.SetValue(ButttonWInfo.ContentProperty, "Открыть");
            DataTemplate cellTemplate2 = new DataTemplate();
            cellTemplate2.VisualTree = buttonFactory;
            buttonColumn.CellTemplate = cellTemplate2;
            firstGrid.Columns.Add(buttonColumn);

            var readers = db.Readers.ToList();
            firstGrid.ItemsSource = readers;


            for (int i = 0; i < readers.Count; i++)
            {
                firstGrid.UpdateLayout();
                firstGrid.ScrollIntoView(firstGrid.Items[i]);
                DataGridRow dataGridRow = (DataGridRow)firstGrid.ItemContainerGenerator.ContainerFromIndex(i);
                List<ButttonWInfo> comboBoxes = FindVisualChildren<ButttonWInfo>(dataGridRow);
                for (int j = 0; j < comboBoxes.Count; j++)
                {
                    comboBoxes[j].SomeId = readers[i].Id;
                }
            }

            firstGrid.Visibility = Visibility.Visible;

        }

        private void ButtonInfo_Click3(object sender, RoutedEventArgs e)
        {
            var button = sender as ButttonWInfo;


            MainWindow examplersWindow = new MainWindow();
            examplersWindow.Show();
            examplersWindow.menu.Visibility = Visibility.Collapsed;


            examplersWindow.firstGrid.Visibility = Visibility.Hidden;
            examplersWindow.firstGrid.ItemsSource = null;
            examplersWindow.firstGrid.Columns.Clear();
            if (strangeEvent == true)
            {
                examplersWindow.firstGrid.RowEditEnding -= FirstGrid_SourceUpdated;
                strangeEvent = false;
            }

            UserInfo info = userInfo.Find(x => x.MenuInfo!.Title == "Читатели")!;
            examplersWindow.Granting_Rights(info);


            DataGridNumericColumn col2 = new DataGridNumericColumn();
            col2.Header = "Инвентарный номер";
            col2.Binding = new Binding("ExamplerId") { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            examplersWindow.firstGrid.Columns.Add(col2);

            DataGridTemplateColumn templateColumn = new DataGridTemplateColumn();
            DataTemplate dataTemplate = new DataTemplate();
            FrameworkElementFactory datePicker = new FrameworkElementFactory(typeof(DatePicker));
            datePicker.SetBinding(DatePicker.SelectedDateProperty, new Binding("DateOfIssue") { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
            datePicker.AddHandler(DatePicker.SelectedDateChangedEvent, new EventHandler<SelectionChangedEventArgs>(examplersWindow.DataGridChanged!));

            dataTemplate.VisualTree = datePicker;
            templateColumn.Header = "День взятия";
            templateColumn.CellTemplate = dataTemplate;
            examplersWindow.firstGrid.Columns.Add(templateColumn);

            DataGridTemplateColumn templateColumn2 = new DataGridTemplateColumn();
            DataTemplate dataTemplate2 = new DataTemplate();
            FrameworkElementFactory datePicker2 = new FrameworkElementFactory(typeof(DatePicker));
            datePicker2.SetBinding(DatePicker.SelectedDateProperty, new Binding("DateOfReturn") { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
            datePicker2.AddHandler(DatePicker.SelectedDateChangedEvent, new EventHandler<SelectionChangedEventArgs>(examplersWindow.DataGridChanged!));

            dataTemplate2.VisualTree = datePicker2;
            templateColumn2.Header = "День возврата";
            templateColumn2.CellTemplate = dataTemplate2;
            examplersWindow.firstGrid.Columns.Add(templateColumn2);

            DataGridCheckBoxColumn checkboxColumn1 = new DataGridCheckBoxColumn();
            checkboxColumn1.Header = "Вернули?";
            checkboxColumn1.Binding = new Binding("Returned") { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            examplersWindow.firstGrid.Columns.Add(checkboxColumn1);



            var examplers = db.GivenExamplers.Where(e => e.ReaderId == button!.SomeId).ToList();


            examplersWindow.firstGrid.RowEditEnding += (sender, e) =>
            {
                var item = e.Row.Item as GivenExamplers;
                item.ReaderId = button!.SomeId;
                examplersWindow.db.GivenExamplers.Add(item);

                
            };
            examplersWindow.firstGrid.ItemsSource = examplers;


            examplersWindow.firstGrid.Visibility = Visibility.Visible;
        }

        private void ButtonInfo_Click4(object sender, RoutedEventArgs e)
        {
            var button = sender as ButttonWInfo;


            MainWindow examplersWindow = new MainWindow();
            examplersWindow.Show();
            examplersWindow.menu.Visibility = Visibility.Collapsed;


            examplersWindow.firstGrid.Visibility = Visibility.Hidden;
            examplersWindow.firstGrid.ItemsSource = null;
            examplersWindow.firstGrid.Columns.Clear();
            if (strangeEvent == true)
            {
                firstGrid.RowEditEnding -= FirstGrid_SourceUpdated;
                strangeEvent = false;
            }


            UserInfo info = userInfo.Find(x => x.MenuInfo!.Title == "Читатели")!;
            examplersWindow.Granting_Rights(info);


            DataGridComboBoxColumn comboColumn5 = new DataGridComboBoxColumn();
            comboColumn5.Header = "Штраф";
            comboColumn5.ItemsSource = db.Penalties.ToList();
            comboColumn5.SelectedValueBinding = new Binding("PenaltyId") { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            comboColumn5.SelectedValuePath = "Id";
            comboColumn5.DisplayMemberPath = "Name";
            examplersWindow.firstGrid.Columns.Add(comboColumn5);


            DataGridTemplateColumn templateColumn = new DataGridTemplateColumn();
            DataTemplate dataTemplate = new DataTemplate();
            FrameworkElementFactory datePicker = new FrameworkElementFactory(typeof(DatePicker));
            datePicker.SetBinding(DatePicker.SelectedDateProperty, new Binding("DateOfGetting") { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
            datePicker.AddHandler(DatePicker.SelectedDateChangedEvent, new EventHandler<SelectionChangedEventArgs>(examplersWindow.DataGridChanged!));
            dataTemplate.VisualTree = datePicker;
            templateColumn.Header = "Дата получения";
            templateColumn.CellTemplate = dataTemplate;
            examplersWindow.firstGrid.Columns.Add(templateColumn);

            DataGridTemplateColumn templateColumn2 = new DataGridTemplateColumn();
            DataTemplate dataTemplate2 = new DataTemplate();
            FrameworkElementFactory datePicker2 = new FrameworkElementFactory(typeof(DatePicker));
            datePicker2.SetBinding(DatePicker.SelectedDateProperty, new Binding("DateOfEnding") { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
            datePicker2.AddHandler(DatePicker.SelectedDateChangedEvent, new EventHandler<SelectionChangedEventArgs>(examplersWindow.DataGridChanged!));
            dataTemplate2.VisualTree = datePicker2;
            templateColumn2.Header = "День окончания";
            templateColumn2.CellTemplate = dataTemplate2;
            examplersWindow.firstGrid.Columns.Add(templateColumn2);


            

            examplersWindow.firstGrid.RowEditEnding += (sender, e) =>
            {
                var item = e.Row.Item as ReadersAndPenaltys;
                item.ReaderId = button!.SomeId;
                examplersWindow.db.ReadersAndPenaltys.Add(item);

                var test = db.ReadersAndPenaltys.ToList();
            };

            var examplers = db.ReadersAndPenaltys.Where(e => e.ReaderId == button!.SomeId).ToList();
            examplersWindow.firstGrid.ItemsSource = examplers;


            examplersWindow.firstGrid.Visibility = Visibility.Visible;
        }

        private void Storage_Click(object sender, RoutedEventArgs e)
        {
            firstGrid.Visibility = Visibility.Hidden;
            firstGrid.ItemsSource = null;
            firstGrid.Columns.Clear();
            if (strangeEvent == true)
            {
                firstGrid.RowEditEnding -= FirstGrid_SourceUpdated;
                strangeEvent = false;
            }
            UserInfo info = userInfo.Find(x => x.MenuInfoId == (sender as MyMenuItem)!.Id)!;
            Granting_Rights(info);

            DataGridNumericColumn col2 = new DataGridNumericColumn();
            col2.Header = "Номер";
            col2.Binding = new Binding("Id");
            firstGrid.Columns.Add(col2);

            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Тип";
            col1.Binding = new Binding("Type");
            firstGrid.Columns.Add(col1);




            firstGrid.ItemsSource = db.Storages.ToList();
            firstGrid.Visibility = Visibility.Visible;
        }
        private void TypeOfComposition_Click(object sender, RoutedEventArgs e)
        {
            firstGrid.Visibility = Visibility.Hidden;
            firstGrid.ItemsSource = null;
            firstGrid.Columns.Clear();
            if (strangeEvent == true)
            {
                firstGrid.RowEditEnding -= FirstGrid_SourceUpdated;
                strangeEvent = false;
            }
            UserInfo info = userInfo.Find(x => x.MenuInfoId == (sender as MyMenuItem)!.Id)!;
            Granting_Rights(info);


            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Название";
            col1.Binding = new Binding("Name");
            firstGrid.Columns.Add(col1);

            DataGridNumericColumn col2 = new DataGridNumericColumn();
            col2.Header = "Дней выдачи";
            col2.Binding = new Binding("DaysOfIssuance");
            firstGrid.Columns.Add(col2);

            DataGridCheckBoxColumn checkboxColumn1 = new DataGridCheckBoxColumn();
            checkboxColumn1.Header = "Разрешение";
            checkboxColumn1.Binding = new Binding("PermissionOnIssuance") { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            firstGrid.Columns.Add(checkboxColumn1);

            DataGridComboBoxColumn comboColumn2 = new DataGridComboBoxColumn();
            comboColumn2.Header = "Хранилище";
            comboColumn2.ItemsSource = db.Storages.ToList();
            comboColumn2.SelectedValueBinding = new Binding("StorageId");
            comboColumn2.SelectedValuePath = "Id";
            comboColumn2.DisplayMemberPath = "Type";
            firstGrid.Columns.Add(comboColumn2);

            firstGrid.ItemsSource = db.TypesOfComposition.ToList();
            firstGrid.Visibility = Visibility.Visible;


        }

        private void City_Click(object sender, RoutedEventArgs e)
        {
            firstGrid.Visibility = Visibility.Hidden;
            firstGrid.ItemsSource = null;
            firstGrid.Columns.Clear();
            if (strangeEvent == true)
            {
                firstGrid.RowEditEnding -= FirstGrid_SourceUpdated;
                strangeEvent = false;
            }
            UserInfo info = userInfo.Find(x => x.MenuInfoId == (sender as MyMenuItem)!.Id)!;
            Granting_Rights(info);


            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Название";
            col1.Binding = new Binding("Name");
            firstGrid.Columns.Add(col1);

            firstGrid.ItemsSource = db.Cities.ToList();
            firstGrid.Visibility = Visibility.Visible;


        }
        private void Degrees_Click(object sender, RoutedEventArgs e)
        {
            firstGrid.Visibility = Visibility.Hidden;
            firstGrid.ItemsSource = null;
            firstGrid.Columns.Clear();
            if (strangeEvent == true)
            {
                firstGrid.RowEditEnding -= FirstGrid_SourceUpdated;
                strangeEvent = false;
            }
            UserInfo info = userInfo.Find(x => x.MenuInfoId == (sender as MyMenuItem)!.Id)!;
            Granting_Rights(info);


            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Название";
            col1.Binding = new Binding("Name");
            firstGrid.Columns.Add(col1);
            firstGrid.ItemsSource = db.Degrees.ToList();
            firstGrid.Visibility = Visibility.Visible;
        }

        private void Departments_Click(object sender, RoutedEventArgs e)
        {
            firstGrid.Visibility = Visibility.Hidden;
            firstGrid.ItemsSource = null;
            firstGrid.Columns.Clear();
            if (strangeEvent == true)
            {
                firstGrid.RowEditEnding -= FirstGrid_SourceUpdated;
                strangeEvent = false;
            }
            UserInfo info = userInfo.Find(x => x.MenuInfoId == (sender as MyMenuItem)!.Id)!;
            Granting_Rights(info);

            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Название";
            col1.Binding = new Binding("Name");
            firstGrid.Columns.Add(col1);

            DataGridComboBoxColumn comboColumn2 = new DataGridComboBoxColumn();
            comboColumn2.Header = "Факультет";
            comboColumn2.ItemsSource = db.Faculties.ToList();
            comboColumn2.SelectedValueBinding = new Binding("FacultyId");
            comboColumn2.SelectedValuePath = "Id";
            comboColumn2.DisplayMemberPath = "Name";
            firstGrid.Columns.Add(comboColumn2);


            firstGrid.ItemsSource = db.Departments.ToList();
            firstGrid.Visibility = Visibility.Visible;
        }

        private void Facultys_Click(object sender, RoutedEventArgs e)
        {
            firstGrid.Visibility = Visibility.Hidden;
            firstGrid.ItemsSource = null;
            firstGrid.Columns.Clear();
            if (strangeEvent == true)
            {
                firstGrid.RowEditEnding -= FirstGrid_SourceUpdated;
                strangeEvent = false;
            }
            UserInfo info = userInfo.Find(x => x.MenuInfoId == (sender as MyMenuItem)!.Id)!;
            Granting_Rights(info);

            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Название";
            col1.Binding = new Binding("Name");
            firstGrid.Columns.Add(col1);
            firstGrid.ItemsSource = db.Faculties.ToList();
            firstGrid.Visibility = Visibility.Visible;
        }

        private void Ranks_Click(object sender, RoutedEventArgs e)
        {
            firstGrid.Visibility = Visibility.Hidden;
            firstGrid.ItemsSource = null;
            firstGrid.Columns.Clear();
            if (strangeEvent == true)
            {
                firstGrid.RowEditEnding -= FirstGrid_SourceUpdated;
                strangeEvent = false;
            }
            UserInfo info = userInfo.Find(x => x.MenuInfoId == (sender as MyMenuItem)!.Id)!;
            Granting_Rights(info);

            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Название";
            col1.Binding = new Binding("Name");
            firstGrid.Columns.Add(col1);
            firstGrid.ItemsSource = db.Ranks.ToList();
            firstGrid.Visibility = Visibility.Visible;
        }

        private void Positions_Click(object sender, RoutedEventArgs e)
        {
            firstGrid.Visibility = Visibility.Hidden;
            firstGrid.ItemsSource = null;
            firstGrid.Columns.Clear();
            if (strangeEvent == true)
            {
                firstGrid.RowEditEnding -= FirstGrid_SourceUpdated;
                strangeEvent = false;
            }
            UserInfo info = userInfo.Find(x => x.MenuInfoId == (sender as MyMenuItem)!.Id)!;
            Granting_Rights(info);

            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Название";
            col1.Binding = new Binding("Name");
            firstGrid.Columns.Add(col1);
            firstGrid.ItemsSource = db.Positions.ToList();
            firstGrid.Visibility = Visibility.Visible;
        }

        private void Authors_Click(object sender, RoutedEventArgs e)
        {
            firstGrid.Visibility = Visibility.Hidden;
            firstGrid.ItemsSource = null;
            firstGrid.Columns.Clear();
            if (strangeEvent == true)
            {
                firstGrid.RowEditEnding -= FirstGrid_SourceUpdated;
                strangeEvent = false;
            }
            UserInfo info = userInfo.Find(x => x.MenuInfoId == (sender as MyMenuItem)!.Id)!;
            Granting_Rights(info);

            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Фамилия";
            col1.Binding = new Binding("LastName");
            firstGrid.Columns.Add(col1);


            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Header = "Имя";
            col2.Binding = new Binding("FirstName");
            firstGrid.Columns.Add(col2);


            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Header = "Отчество";
            col3.Binding = new Binding("MiddleName");
            firstGrid.Columns.Add(col3);

            firstGrid.ItemsSource = db.Authors.ToList();
            firstGrid.Visibility = Visibility.Visible;
        }

        private void AuthorsAndCompositions_Click(object sender, RoutedEventArgs e)
        {
            firstGrid.Visibility = Visibility.Hidden;
            firstGrid.ItemsSource = null;
            firstGrid.Columns.Clear();
            if (strangeEvent == true)
            {
                firstGrid.RowEditEnding -= FirstGrid_SourceUpdated;
                strangeEvent = false;
            }
            UserInfo info = userInfo.Find(x => x.MenuInfoId == (sender as MyMenuItem)!.Id)!;
            Granting_Rights(info);

            List<Author> authors = db.Authors.ToList();
            List<ReaderFullName> readerFullNames = new List<ReaderFullName>();
            for (int i = 0; i < authors.Count; i++)
            {
                readerFullNames.Add(new ReaderFullName(authors[i].FirstName,
                    authors[i].LastName, authors[i].MiddleName)
                { Id = authors[i].Id });
            }
            DataGridComboBoxColumn comboColumn3 = new DataGridComboBoxColumn();
            comboColumn3.Header = "Автор";
            comboColumn3.ItemsSource = readerFullNames;
            comboColumn3.SelectedValueBinding = new Binding("AuthorId");
            comboColumn3.SelectedValuePath = "Id";
            comboColumn3.DisplayMemberPath = "FullName";
            firstGrid.Columns.Add(comboColumn3);

            DataGridComboBoxColumn comboColumn2 = new DataGridComboBoxColumn();
            comboColumn2.Header = "Произведение";
            comboColumn2.ItemsSource = db.Compositions.ToList();
            comboColumn2.SelectedValueBinding = new Binding("CompositionId");
            comboColumn2.SelectedValuePath = "Id";
            comboColumn2.DisplayMemberPath = "Name";
            firstGrid.Columns.Add(comboColumn2);


            firstGrid.ItemsSource = db.AuthorsAndCompositions.ToList();
            firstGrid.Visibility = Visibility.Visible;
        }
        private void UserInfo_Click(object sender, RoutedEventArgs e)
        {
            firstGrid.Visibility = Visibility.Hidden;
            firstGrid.ItemsSource = null;
            firstGrid.Columns.Clear();
            firstGrid.CanUserAddRows = false;
            editCurrentMenu = true;
            if (strangeEvent == true)
            {
                firstGrid.RowEditEnding -= FirstGrid_SourceUpdated;
                strangeEvent = false;
            }
            //UserInfo info = userInfo.Find(x => x.MenuInfoId == (sender as MyMenuItem)!.Id)!;
            //Granting_Rights(info);

            DataGridComboBoxColumn comboColumn2 = new DataGridComboBoxColumn();
            comboColumn2.Header = "Логин";
            comboColumn2.ItemsSource = db.Users.ToList();
            comboColumn2.SelectedValueBinding = new Binding("UserId");
            comboColumn2.SelectedValuePath = "Id";
            comboColumn2.DisplayMemberPath = "Name";
            comboColumn2.IsReadOnly = true;
            firstGrid.Columns.Add(comboColumn2);

            DataGridComboBoxColumn comboColumn1 = new DataGridComboBoxColumn();
            comboColumn1.Header = "Меню";
            comboColumn1.ItemsSource = menuInfos;
            comboColumn1.SelectedValueBinding = new Binding("MenuInfoId");
            comboColumn1.SelectedValuePath = "Id";
            comboColumn1.DisplayMemberPath = "Title";
            comboColumn1.IsReadOnly = true;
            firstGrid.Columns.Add(comboColumn1);

            DataGridCheckBoxColumn checkboxColumn1 = new DataGridCheckBoxColumn();
            checkboxColumn1.Header = "Чтение";
            checkboxColumn1.Binding = new Binding("Read") { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            firstGrid.Columns.Add(checkboxColumn1);

            DataGridCheckBoxColumn checkboxColumn2 = new DataGridCheckBoxColumn();
            checkboxColumn2.Header = "Запись";
            checkboxColumn2.Binding = new Binding("Write") { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            firstGrid.Columns.Add(checkboxColumn2);

            DataGridCheckBoxColumn checkboxColumn3 = new DataGridCheckBoxColumn();
            checkboxColumn3.Header = "Редактирование";
            checkboxColumn3.Binding = new Binding("Edit") { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            firstGrid.Columns.Add(checkboxColumn3);

            DataGridCheckBoxColumn checkboxColumn4 = new DataGridCheckBoxColumn();
            checkboxColumn4.Header = "Удаление";
            checkboxColumn4.Binding = new Binding("Delete") { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            firstGrid.Columns.Add(checkboxColumn4);



            firstGrid.ItemsSource = db.UserInfo.ToList();

            firstGrid.Visibility = Visibility.Visible;
        }

        private void User_Click(object sender, RoutedEventArgs e)
        {
            firstGrid.Visibility = Visibility.Hidden;
            firstGrid.ItemsSource = null;
            firstGrid.Columns.Clear();
            if (strangeEvent == false)
            {
                firstGrid.RowEditEnding += FirstGrid_SourceUpdated;
                strangeEvent = true;
            }
            UserInfo info = userInfo.Find(x => x.MenuInfoId == (sender as MyMenuItem)!.Id)!;
            Granting_Rights(info);

            Binding bind = new Binding("Name");
            bind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Логин";
            col1.Binding = bind;
            firstGrid.Columns.Add(col1);
            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Header = "Пароль";
            col2.Binding = new Binding("Password");
            firstGrid.Columns.Add(col2);


            firstGrid.ItemsSource = db.Users.ToList();
            firstGrid.Visibility = Visibility.Visible;
        }

        private void FirstGrid_SourceUpdated(object? sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                using (ApplicationContext someDb = new ApplicationContext())
                {
                    var grid = (DataGrid)sender!;
                    User user2 = (e.Row.Item as User)!;
                    someDb.Add(user2);
                    someDb.SaveChanges();
                    someDb.Remove(user2);
                    someDb.SaveChanges();
                    if (grid.SelectedItems.Count > 0)
                    {

                        foreach (var row in grid.SelectedItems)
                        {
                            User user = (row as User)!;
                            for (int i = 0; i < menuInfos.Count; i++)
                            {
                                UserInfo userInfo = new UserInfo() { UserId = user!.Id, MenuInfoId = menuInfos[i].Id };
                                db.UserInfo.Add(userInfo);
                            }

                        }
                    }
                }
            }
        }


        private void ForgetPassword_Click(object sender, RoutedEventArgs e)
        {
            PasswordRecovery passwordRecovery = new PasswordRecovery();
            passwordRecovery.Owner = this;
            passwordRecovery.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            db.Dispose();
            dbForUser.Dispose();
        }
        private void Granting_Rights(UserInfo info)
        {
            if (info.Write == false)
            { firstGrid.CanUserAddRows = false; }
            else
            { firstGrid.CanUserAddRows = true; }

            if (info.Edit == false)
            { editCurrentMenu = false; }
            else
            { editCurrentMenu = true; }

            if (info.Delete == false)
            { firstGrid.CanUserDeleteRows = false; }
            else { firstGrid.CanUserDeleteRows = true; }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (editCurrentMenu == false)
            {
                firstGrid.IsReadOnly = true;
                MessageBox.Show("У вас нет прав на редактирование данных в этой таблице");
                return;
            }

            if (edit == false)
            {
                edit = true;
                var button = (Button)sender!;
                firstGrid.IsReadOnly = false;
                firstGrid.IsLocked = true;

                button.Content = "Завершить редактирование";
            }
            else
            {
                edit = false;
                var button = (Button)sender!;
                firstGrid.IsReadOnly = true;
                firstGrid.IsLocked = false;

                button.Content = "Редактировать";
            }
        }
        private void AboutProgramm_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Добро пожаловать в информационную систему бибилиотеки ВУЗА. После того как вы зашли в систему под вашим логином и паролем, вы можете выбрать один из пунктов меню, которые находятся выше. Далее у вас появится таблица с данными, чтобы редактировать или добавлять данные нужно нажать кнопку \"Редактировать\", после завершения редактирования нажмите кнопку \"Завершить редактирование\" и \"Сохранить\". После этого все внесенные изменения сохраняться.");
        }

        private void UserHelp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Редактирование таблицы:\r\n1. Внесение новой записи, например: добавлять данные о новом студенте в таблицу студентов\r\n2. Редактирование записи, например: изменение фамилии или должности профессора в таблице\r\n3. Удаление записи, например: удалить запись из таблицы\r\nАвторизация:\r\n1. Авторизация пользователя путём ввода логина и пароля, по логину определяется роль пользователя в системе\r\n2. Смена пароля: позволяет сменить пароль пользователя. Для этого нужно внести используемый пароль на данный момент и подтвердить новый\r\n");
        }


    }
}

