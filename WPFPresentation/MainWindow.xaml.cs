using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataAccessFakes;
using DataAccessInterfaces;
using DataObjects;
using LogicLayer;

namespace WPFPresentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User _user = null;
        UserCard _userCard;
        Deck _deck = null;
        MatchDeck _matchDeck;
        Match _match = null;

        UserManager _userManager = null;
        CardManager _cardManager = null;
        DeckManager _deckManager = null;
        MatchManager _matchManager = null;
        private int rowCounter = 20;

        private int pageNumber;
        //MainWindow newWindow = null;

        public MainWindow()
        {
            //_userManager = new UserManager();
            _userManager = new UserManager(new DataAccessFakes.UserAccessorFake());
            //newWindow = frmMainWindow;

            //_cardManager = new CardManager();
            _cardManager = new CardManager(new DataAccessFakes.CardAccessorFake());

            //_deckManager = new DeckManager();
            _deckManager = new DeckManager(new DataAccessFakes.DeckAccessorFake());

            //_matchManager = new MatchManager();
            _matchManager = new MatchManager(new DataAccessFakes.MatchAccessorFake());

            InitializeComponent();
            AppData.DataPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\" + "Images";
            btnHome.Focus();
        }

        private void hideAllButtons()
        {
            btnCards.Visibility = Visibility.Collapsed;
            btnDecks.Visibility = Visibility.Collapsed;
            btnMatches.Visibility = Visibility.Collapsed;
            btnMyStuff.Visibility = Visibility.Collapsed;
            mnuStuff.Visibility = Visibility.Collapsed;
        }

        private void showAllButtons()
        {
            btnCards.Visibility = Visibility.Visible;
            btnDecks.Visibility = Visibility.Visible;
            btnMatches.Visibility = Visibility.Visible;
            btnMyStuff.Visibility = Visibility.Visible;
            mnuStuff.Visibility = Visibility.Visible;
        }

        private void updateUIForLogOut()
        {
            _user = null;
            btnLogin.Content = "Login";
            hideAllButtons();
            btnHome.Focus();
            mnuLogout.Visibility = Visibility.Collapsed;
            datMyCards.ItemsSource = null;
            datMyDecks.ItemsSource = null;
            datMyMatches.ItemsSource = null;
            staMessage.Content = "Welcome. Please log in to continue.";
        }

        private void updateUIForLogin()
        {
            staMessage.Content = "Welcome, " + _user.Username;
            btnLogin.Content = "Log out";
            mnuLogout.Visibility = Visibility.Visible;
            showAllButtons();
        }

        private void hideAllDetails()
        {
            grdHome.Visibility = Visibility.Collapsed;
            grdCards.Visibility = Visibility.Collapsed;
            grdNextPrev.Visibility = Visibility.Collapsed;
            grdDecks.Visibility = Visibility.Collapsed;
            grdDeckCards.Visibility = Visibility.Collapsed;
            grdMatches.Visibility = Visibility.Collapsed;
            grdMatchDecks.Visibility = Visibility.Collapsed;
            grdMatchDeckCards.Visibility = Visibility.Collapsed;
            grdMyCards.Visibility = Visibility.Collapsed;
            grdMyDecks.Visibility = Visibility.Collapsed;
            grdMyMatches.Visibility = Visibility.Collapsed;
            grdButtons.Visibility = Visibility.Collapsed;
            grdLabels.Visibility = Visibility.Collapsed;
            //grdMyStuff.Visibility = Visibility.Collapsed;
            datMyCards.Visibility = Visibility.Collapsed;
            datMyDecks.Visibility = Visibility.Collapsed;
            datMyDeckCards.Visibility = Visibility.Collapsed;
            datMyMatches.Visibility = Visibility.Collapsed;
            datMyMatchDecks.Visibility = Visibility.Collapsed;
            datMyMatchDeckCards.Visibility = Visibility.Collapsed;
            btnCreateDeck.Visibility = Visibility.Collapsed;
            btnUpdateDeck.Visibility = Visibility.Collapsed;
            btnDeleteDeck.Visibility = Visibility.Collapsed;
            btnUpdateDeckCard.Visibility = Visibility.Collapsed;
            btnDeleteDeckCard.Visibility = Visibility.Collapsed;
            btnCreateMatch.Visibility = Visibility.Collapsed;
            btnUpdateMatch.Visibility = Visibility.Collapsed;
            btnDeleteMatch.Visibility = Visibility.Collapsed;
            btnAddMatchDeck.Visibility = Visibility.Collapsed;
            btnDeleteMatchDeck.Visibility = Visibility.Collapsed;
            grdLabels.Visibility = Visibility.Collapsed;
            lblMyDeckName.Visibility = Visibility.Collapsed;
            lblMyMatchName.Visibility = Visibility.Collapsed;
            lblTabDetail.Visibility = Visibility.Collapsed;

        }

        private void pageChecker()
        {
            if (pageNumber == 1)
            {
                btnPrevPage.IsEnabled = false;
            }
            else
            {
                btnPrevPage.IsEnabled = true;
            }
            if (grdCards.Visibility == Visibility.Visible)
            {                                                           // the amount of cards / rows being retrieved 
                if (_cardManager.RetrieveCardsByPage(pageNumber).Count < rowCounter)
                {
                    btnNextPage.IsEnabled = false;
                }
                else
                {
                    btnNextPage.IsEnabled = true;
                }
            }
            else if (grdDecks.Visibility == Visibility.Visible)
            {
                if (_deckManager.RetrieveDecksByPage(pageNumber).Count < rowCounter)
                {
                    btnNextPage.IsEnabled = false;
                }
                else
                {
                    btnNextPage.IsEnabled = true;
                }
            }
            else if (grdMatches.Visibility == Visibility.Visible)
            {
                if (_matchManager.RetrieveMatchesByPage(pageNumber).Count < rowCounter)
                {
                    btnNextPage.IsEnabled = false;
                }
                else
                {
                    btnNextPage.IsEnabled = true;
                }
            }
            else if (datMyCards.Visibility == Visibility.Visible)
            {
                if (_cardManager.RetrieveUserCardsByUserID(_user.UserID, pageNumber).Count < rowCounter)
                {
                    btnNextPage.IsEnabled = false;
                }
                else
                {
                    btnNextPage.IsEnabled = true;
                }
            }
            else if (datMyDecks.Visibility == Visibility.Visible)
            {
                if (_deckManager.RetrieveUserDecksByUserID(_user.UserID, pageNumber).Count < rowCounter)
                {
                    btnNextPage.IsEnabled = false;
                }
                else
                {
                    btnNextPage.IsEnabled = true;
                }
            }
            else if (datMyMatches.Visibility == Visibility.Visible)
            {
                if (_matchManager.RetrieveUserMatchesByUserID(_user.UserID, pageNumber).Count < rowCounter)
                {
                    btnNextPage.IsEnabled = false;
                }
                else
                {
                    btnNextPage.IsEnabled = true;
                }
            }
        }

        private void PrevNextPageClick_Helper()
        {
            pageChecker();
            if (grdCards.Visibility == Visibility.Visible)
            {
                //datCards.ItemsSource = _cardManager.RetrieveCardsByPage(pageNumber);
                datCards.ItemsSource = createCardList();
            }
            else if (grdDecks.Visibility == Visibility.Visible)
            {
                datDecks.ItemsSource = _deckManager.RetrieveDecksByPage(pageNumber);
            }
            else if (grdMatches.Visibility == Visibility.Visible)
            {
                datMatches.ItemsSource = _matchManager.RetrieveMatchesByPage(pageNumber);
            }
            else if (datMyCards.Visibility == Visibility.Visible)
            {
                datMyCards.ItemsSource = _cardManager.RetrieveUserCardsByUserID(_user.UserID, pageNumber);
            }
            else if (datMyDecks.Visibility == Visibility.Visible)
            {
                datMyDecks.ItemsSource = _deckManager.RetrieveUserDecksByUserID(_user.UserID, pageNumber);
            }
            else if (datMyMatches.Visibility == Visibility.Visible)
            {
                datMyMatches.ItemsSource = _matchManager.RetrieveUserMatchesByUserID(_user.UserID, pageNumber);
            }
        }

        private void clearDataGrids()
        {
            datMyCards.ItemsSource = null;
            datMyDecks.ItemsSource = null;
            datMyMatches.ItemsSource = null;
        }

        private void loadDetailWindow(UserCard userCard)
        {
            var cardDetailWindow = new CardDetail(userCard, _cardManager, _deckManager, _matchManager);
            cardDetailWindow.ShowDialog();

            cardsHelper();
        }

        private List<UserCard> createCardList()
        {
            List<UserCard> displayCards = new List<UserCard>();

            List<UserCard> userCards = _cardManager.RetrieveUserCardsByUserID(_user.UserID, 0);
            List<Cards> cards = _cardManager.RetrieveCardsByPage(pageNumber);

            foreach (Cards card in cards)
            {
                for (int i = 0; i <= userCards.Count; i++)
                {
                    if (i == userCards.Count)
                    {
                        displayCards.Add(new UserCard()
                        {
                            UserID = _user.UserID,
                            CardID = card.CardID,
                            CardName = card.CardName,
                            ImageID = card.ImageID,
                            CardDescription = card.CardDescription,
                            CardColorID = card.CardColorID,
                            CardConvertedManaCost = card.CardConvertedManaCost,
                            CardRarityID = card.CardRarityID,
                            CardTypeID = card.CardTypeID,
                            HasSecondaryCard = card.HasSecondaryCard,
                            CardSecondaryName = card.CardSecondaryName,
                            SecondaryImageID = card.SecondaryImageID,
                            CardSecondaryDescription = card.CardSecondaryDescription,
                            CardSecondaryColorID = card.CardSecondaryColorID,
                            CardSecondaryConvertedManaCost = card.CardSecondaryConvertedManaCost,
                            CardSecondaryRarityID = card.CardSecondaryRarityID,
                            CardSecondaryTypeID = card.CardSecondaryTypeID,
                            OwnedCard = false,
                            Wishlisted = false
                        });
                        break;
                    }
                    if (userCards[i].CardID == card.CardID)
                    {
                        displayCards.Add(userCards[i]);
                        break;
                    }
                }
            }

            return displayCards;
        }

        private List<UserCard> createCardList(Deck deck)
        {
            List<UserCard> displayCards = new List<UserCard>();

            List<UserCard> userCards = _cardManager.RetrieveUserCardsByUserID(_user.UserID, 0);
            List<DeckCard> cards = _deckManager.RetrieveDeckCards(deck.DeckID);


            foreach (DeckCard card in cards)
            {
                for (int i = 0; i <= userCards.Count; i++)
                {
                    if (i == userCards.Count)
                    {
                        displayCards.Add(new UserCard()
                        {
                            UserID = _user.UserID,
                            CardID = card.CardID,
                            CardCount = card.CardCount,
                            CardName = card.CardName,
                            ImageID = card.ImageID,
                            CardDescription = card.CardDescription,
                            CardColorID = card.CardColorID,
                            CardConvertedManaCost = card.CardConvertedManaCost,
                            CardRarityID = card.CardRarityID,
                            CardTypeID = card.CardTypeID,
                            HasSecondaryCard = card.HasSecondaryCard,
                            CardSecondaryName = card.CardSecondaryName,
                            SecondaryImageID = card.SecondaryImageID,
                            CardSecondaryDescription = card.CardSecondaryDescription,
                            CardSecondaryColorID = card.CardSecondaryColorID,
                            CardSecondaryConvertedManaCost = card.CardSecondaryConvertedManaCost,
                            CardSecondaryRarityID = card.CardSecondaryRarityID,
                            CardSecondaryTypeID = card.CardSecondaryTypeID,
                            OwnedCard = false,
                            Wishlisted = false
                        });
                        break;
                    }
                    if (userCards[i].CardID == card.CardID)
                    {
                        displayCards.Add(userCards[i]);
                        displayCards[displayCards.Count - 1].CardCount = card.CardCount;
                        break;
                    }
                }
            }


            return displayCards;
        }

        private List<UserCard> createCardList(MatchDeck deck)
        {
            List<UserCard> displayCards = new List<UserCard>();

            List<UserCard> userCards = _cardManager.RetrieveUserCardsByUserID(_user.UserID, 0);
            List<DeckCard> cards = _deckManager.RetrieveDeckCards(deck.DeckID);


            foreach (DeckCard card in cards)
            {
                for (int i = 0; i <= userCards.Count; i++)
                {
                    if (i == userCards.Count)
                    {
                        displayCards.Add(new UserCard()
                        {
                            UserID = _user.UserID,
                            CardID = card.CardID,
                            CardCount = card.CardCount,
                            CardName = card.CardName,
                            ImageID = card.ImageID,
                            CardDescription = card.CardDescription,
                            CardColorID = card.CardColorID,
                            CardConvertedManaCost = card.CardConvertedManaCost,
                            CardRarityID = card.CardRarityID,
                            CardTypeID = card.CardTypeID,
                            HasSecondaryCard = card.HasSecondaryCard,
                            CardSecondaryName = card.CardSecondaryName,
                            SecondaryImageID = card.SecondaryImageID,
                            CardSecondaryDescription = card.CardSecondaryDescription,
                            CardSecondaryColorID = card.CardSecondaryColorID,
                            CardSecondaryConvertedManaCost = card.CardSecondaryConvertedManaCost,
                            CardSecondaryRarityID = card.CardSecondaryRarityID,
                            CardSecondaryTypeID = card.CardSecondaryTypeID,
                            OwnedCard = false,
                            Wishlisted = false
                        });
                        break;
                    }
                    if (userCards[i].CardID == card.CardID)
                    {
                        displayCards.Add(userCards[i]);
                        displayCards[displayCards.Count - 1].CardCount = card.CardCount;
                        break;
                    }
                }
            }


            return displayCards;
        }

        private List<Deck> UserDeckRetrieveHelper()
        {
            List<Deck> decks = new List<Deck>();
            decks = _deckManager.RetrieveUserDecksByUserID(_user.UserID, pageNumber);
            return decks;
        }

        private void myDecksHelper()
        {
            try
            {
                pageNumber = 1;
                hideAllDetails();
                //grdMyStuff.Visibility = Visibility.Visible;
                grdMyDecks.Visibility = Visibility.Visible;
                datMyDecks.Visibility = Visibility.Visible;
                grdNextPrev.Visibility = Visibility.Visible;
                pageChecker();
                btnUpdateDeck.Visibility = Visibility.Visible;
                btnDeleteDeck.Visibility = Visibility.Visible;
                grdButtons.Visibility = Visibility.Visible;
                btnCreateDeck.Visibility = Visibility.Visible;
                datMyDecks.ItemsSource = UserDeckRetrieveHelper();
                btnUpdateDeck.IsEnabled = false;
                btnDeleteDeck.IsEnabled = false;
                grdLabels.Visibility = Visibility.Visible;
                lblTabDetail.Visibility = Visibility.Visible;
                lblTabDetail.Content = "My Decks";
            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex);
            }
        }

        private void myDeckCardsHelper()
        {
            try
            {
                hideAllDetails();
                //grdMyStuff.Visibility = Visibility.Visible;
                grdMyDecks.Visibility = Visibility.Visible;
                datMyDeckCards.Visibility = Visibility.Visible;
                grdButtons.Visibility = Visibility.Visible;


                //datMyDeckCards.ItemsSource = _deckManager.RetrieveDeckCards(deck.DeckID);
                btnUpdateDeck.Visibility = Visibility.Collapsed;
                btnDeleteDeck.Visibility = Visibility.Collapsed;
                btnUpdateDeckCard.Visibility = Visibility.Visible;
                btnDeleteDeckCard.Visibility = Visibility.Visible;
                btnCreateDeck.Visibility = Visibility.Collapsed;
                datMyDeckCards.ItemsSource = createCardList(_deck);
                grdLabels.Visibility = Visibility.Visible;
                lblMyDeckName.Visibility = Visibility.Visible;
                lblMyDeckName.Content = _deck.DeckName;
                btnUpdateDeckCard.IsEnabled = false;
                btnDeleteDeckCard.IsEnabled = false;
                btnAddMatchDeck.Visibility = Visibility.Visible;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void myMatchesHelper()
        {
            try
            {
                hideAllDetails();
                //grdMyStuff.Visibility = Visibility.Visible;
                grdMyMatches.Visibility = Visibility.Visible;
                datMyMatches.Visibility = Visibility.Visible;
                grdButtons.Visibility = Visibility.Visible;
                btnCreateMatch.Visibility = Visibility.Visible;
                btnUpdateMatch.Visibility = Visibility.Visible;
                btnDeleteMatch.Visibility = Visibility.Visible;
                grdNextPrev.Visibility = Visibility.Visible;
                pageChecker();
                datMyMatches.ItemsSource = _matchManager.RetrieveUserMatchesByUserID(_user.UserID, pageNumber);
                btnUpdateMatch.IsEnabled = false;
                btnDeleteMatch.IsEnabled = false;
                grdLabels.Visibility = Visibility.Visible;
                lblTabDetail.Visibility = Visibility.Visible;
                lblTabDetail.Content = "My Matches";
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void myMatchDecksHelper()
        {
            hideAllDetails();
            //grdMyStuff.Visibility = Visibility.Visible;
            grdMyMatches.Visibility = Visibility.Visible;
            datMyMatchDecks.Visibility = Visibility.Visible;

            grdButtons.Visibility = Visibility.Visible;
            btnDeleteMatchDeck.Visibility = Visibility.Visible;
            btnDeleteMatchDeck.IsEnabled = false;

            grdLabels.Visibility = Visibility.Visible;
            lblMyMatchName.Visibility = Visibility.Visible;
            lblMyMatchName.Content = _match.MatchName;

            datMyMatchDecks.ItemsSource = _matchManager.RetrieveMatchDecksByMatchID(_match.MatchID);
        }

        private void cardsHelper()
        {

            if (grdCards.Visibility == Visibility.Visible)
            {
                try
                {
                    hideAllDetails();
                    grdCards.Visibility = Visibility.Visible;
                    grdNextPrev.Visibility = Visibility.Visible;
                    pageChecker();
                    //datCards.ItemsSource = _cardManager.RetrieveCardsByPage(pageNumber);
                    grdLabels.Visibility = Visibility.Visible;
                    lblTabDetail.Visibility = Visibility.Visible;
                    lblTabDetail.Content = "Cards";
                    datCards.ItemsSource = createCardList();
                    //clearDataGrids();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else if (grdDeckCards.Visibility == Visibility.Visible)
            {
                try
                {
                    hideAllDetails();
                    grdDeckCards.Visibility = Visibility.Visible;
                    grdLabels.Visibility = Visibility.Visible;
                    lblMyDeckName.Visibility = Visibility.Visible;
                    lblMyDeckName.Content = _deck.DeckName;

                    //datDeckCards.ItemsSource = _deckManager.RetrieveDeckCards(deck.DeckID);
                    datDeckCards.ItemsSource = createCardList(_deck);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else if (grdMatchDeckCards.Visibility == Visibility.Visible)
            {
                try
                {
                    hideAllDetails();
                    grdMatchDeckCards.Visibility = Visibility.Visible;

                    //datMatchDeckCards.ItemsSource = _deckManager.RetrieveDeckCards(deck.DeckID);
                    datMatchDeckCards.ItemsSource = createCardList(_matchDeck);
                    grdLabels.Visibility = Visibility.Visible;
                    lblMyDeckName.Visibility = Visibility.Visible;
                    lblMyDeckName.Content = _matchDeck.DeckName;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else if (datMyCards.Visibility == Visibility.Visible)
            {
                try
                {
                    hideAllDetails();
                    //grdMyStuff.Visibility = Visibility.Visible;
                    grdMyCards.Visibility = Visibility.Visible;
                    datMyCards.Visibility = Visibility.Visible;
                    grdNextPrev.Visibility = Visibility.Visible;
                    pageChecker();
                    grdLabels.Visibility = Visibility.Visible;
                    lblTabDetail.Visibility = Visibility.Visible;
                    lblTabDetail.Content = "My Cards";
                    datMyCards.ItemsSource = _cardManager.RetrieveUserCardsByUserID(_user.UserID, pageNumber);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else if (datMyDeckCards.Visibility == Visibility.Visible)
            {
                try
                {
                    myDeckCardsHelper();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else if (datMyMatchDeckCards.Visibility == Visibility.Visible)
            {
                try
                {
                    hideAllDetails();
                    //grdMyStuff.Visibility = Visibility.Visible;
                    grdMyMatches.Visibility = Visibility.Visible;
                    datMyMatchDeckCards.Visibility = Visibility.Visible;

                    //datMyMatchDeckCards.ItemsSource = _deckManager.RetrieveDeckCards(deck.DeckID);
                    datMyMatchDeckCards.ItemsSource = createCardList(_matchDeck);
                    grdLabels.Visibility = Visibility.Visible;
                    lblMyDeckName.Visibility = Visibility.Visible;
                    lblMyDeckName.Content = _matchDeck.DeckName;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (btnLogin.Content.ToString() == "Login")
            {
                var logInOrSignup = new LoginOrSignup(_userManager);
                bool? result = logInOrSignup.ShowDialog();
                _user = logInOrSignup.getUser();
                if (result == true)
                {
                    MessageBox.Show("Welcome, " + _user.Username, "Login Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                    updateUIForLogin();
                }
                else
                {
                    _user = null;
                    updateUIForLogOut();
                }
            }
            else
            {
                updateUIForLogOut();
            }
        }

        private void frmMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            hideAllButtons();
        }

        private void btnHome_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                hideAllDetails();
                grdHome.Visibility = Visibility.Visible;
                try
                {
                    Uri src = new Uri(AppData.DataPath + @"\" + "Crimson_Vow_logo.png", UriKind.Absolute);
                    BitmapImage img = new BitmapImage(src);
                    imgInnistrad.Source = img;
                }
                catch (Exception)
                {
                    Uri src = new Uri(AppData.DataPath + @"\" + "nope-not-here.png", UriKind.Absolute);
                    BitmapImage img = new BitmapImage(src);
                    imgInnistrad.Source = img;
                }
                try
                {
                    Uri src = new Uri(AppData.DataPath + @"\" + "Midnight_Hunt_logo.png", UriKind.Absolute);
                    BitmapImage img = new BitmapImage(src);
                    imgMidnight.Source = img;
                }
                catch (Exception)
                {
                    Uri src = new Uri(AppData.DataPath + @"\" + "nope-not-here.png", UriKind.Absolute);
                    BitmapImage img = new BitmapImage(src);
                    imgMidnight.Source = img;
                }
                //clearDataGrids();
            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex);
            }
        }

        private void btnCards_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                pageNumber = 1;
                hideAllDetails();
                grdCards.Visibility = Visibility.Visible;
                grdNextPrev.Visibility = Visibility.Visible;

                cardsHelper();

            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex);
            }
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pageNumber++;
                PrevNextPageClick_Helper();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        private void btnPrevPage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pageNumber--;
                PrevNextPageClick_Helper();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        private void btnDecks_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                pageNumber = 1;
                hideAllDetails();
                grdDecks.Visibility = Visibility.Visible;
                grdNextPrev.Visibility = Visibility.Visible;
                pageChecker();
                grdLabels.Visibility = Visibility.Visible;
                lblTabDetail.Visibility = Visibility.Visible;
                lblTabDetail.Content = "Decks";
                datDecks.ItemsSource = _deckManager.RetrieveDecksByPage(pageNumber);
                //clearDataGrids();
            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex);
            }
        }

        private void datDecks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(datDecks.SelectedItem == null)
            {
                return;
            }
            try
            {
                var deck = (Deck)datDecks.SelectedItem;
                _deck = deck;
                hideAllDetails();
                grdDeckCards.Visibility = Visibility.Visible;
                datDeckCards.Visibility = Visibility.Visible;

                cardsHelper();
            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex);
            }

        }

        private void btnMatches_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                pageNumber = 1;
                hideAllDetails();
                grdMatches.Visibility = Visibility.Visible;
                grdNextPrev.Visibility = Visibility.Visible;
                pageChecker();
                datMatches.ItemsSource = _matchManager.RetrieveMatchesByPage(pageNumber);
                grdLabels.Visibility = Visibility.Visible;
                lblTabDetail.Visibility = Visibility.Visible;
                lblTabDetail.Content = "Matches";
                //clearDataGrids();
            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex);
            }
        }

        private void datMatches_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(datMatches.SelectedItem == null)
            {
                return;
            }
            try
            {
                var match = (Match)datMatches.SelectedItem;

                hideAllDetails();
                grdMatchDecks.Visibility = Visibility.Visible;

                datMatchDecks.ItemsSource = _matchManager.RetrieveMatchDecksByMatchID(match.MatchID);
                grdLabels.Visibility = Visibility.Visible;
                lblMyMatchName.Visibility = Visibility.Visible;
                lblMyMatchName.Content = match.MatchName;
            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex);
            }
        }

        private void datMatchDecks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(datMatchDecks.SelectedItem == null)
            {
                return;
            }
            try
            {
                var deck = (MatchDeck)datMatchDecks.SelectedItem;
                _matchDeck = deck;
                hideAllDetails();
                grdMatchDeckCards.Visibility = Visibility.Visible;
                datMatchDeckCards.Visibility = Visibility.Visible;

                cardsHelper();
                grdLabels.Visibility = Visibility.Visible;
                lblMyDeckName.Visibility = Visibility.Visible;
                lblMyDeckName.Content = deck.DeckName;
            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex);
            }
        }

        private void btnMyStuff_Click(object sender, RoutedEventArgs e)
        {
            mnuMyStuff.IsSubmenuOpen = true;
        }

        private void mnuMyCards_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pageNumber = 1;
                hideAllDetails();
                //grdMyStuff.Visibility = Visibility.Visible;
                grdMyCards.Visibility = Visibility.Visible;
                datMyCards.Visibility = Visibility.Visible;
                grdNextPrev.Visibility = Visibility.Visible;

                cardsHelper();
            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex);
            }
        }

        private void mnuMyDecks_Click(object sender, RoutedEventArgs e)
        {
            myDecksHelper();
        }

        private void datMyDecks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(datMyDecks.SelectedItem == null)
            {
                return;
            }
            try
            {
                var deck = (Deck)datMyDecks.SelectedItem;

                _deck = deck;
                hideAllDetails();
                //grdMyStuff.Visibility = Visibility.Visible;
                grdMyMatches.Visibility = Visibility.Visible;
                datMyMatchDeckCards.Visibility = Visibility.Visible;

                myDeckCardsHelper();
            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex);
            }
        }

        private void mnuMyMatches_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pageNumber = 1;
                myMatchesHelper();

            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex);
            }
        }

        private void datMyMatches_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(datMyMatches.SelectedItem == null)
            {
                return;
            }
            try
            {
                var match = (Match)datMyMatches.SelectedItem;
                _match = match;
                hideAllDetails();
                //grdMyStuff.Visibility = Visibility.Visible;
                grdMyMatches.Visibility = Visibility.Visible;
                datMyMatchDecks.Visibility = Visibility.Visible;

                myMatchDecksHelper();
            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex);
            }
        }

        private void datMyMatchDecks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(datMyMatchDecks.SelectedItem == null)
            {
                return;
            }
            try
            {
                var deck = (MatchDeck)datMyMatchDecks.SelectedItem;
                _matchDeck = deck;
                datMyMatchDeckCards.Visibility = Visibility.Visible;

                cardsHelper();
                grdLabels.Visibility = Visibility.Visible;
                lblMyDeckName.Visibility = Visibility.Visible;
                lblMyDeckName.Content = deck.DeckName;
            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex);
            }
        }


        private void datCards_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(datCards.SelectedItem == null)
            {
                return;
            }
            var userCard = (UserCard)datCards.SelectedItem;
            loadDetailWindow(userCard);
            //btnCards.Focus();
        }

        private void datDeckCards_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(datDeckCards.SelectedItem == null)
            {
                return;
            }
            var userCard = (UserCard)datDeckCards.SelectedItem;
            loadDetailWindow(userCard);

        }

        private void datMatchDeckCards_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(datMatchDeckCards.SelectedItem == null)
            {
                return;
            }
            var userCard = (UserCard)datMatchDeckCards.SelectedItem;
            loadDetailWindow(userCard);
        }

        private void datMyCards_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(datMyCards.SelectedItem == null)
            {
                return;
            }
            var userCard = (UserCard)datMyCards.SelectedItem;
            loadDetailWindow(userCard);
        }

        private void datMyDeckCards_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(datMyDeckCards.SelectedItem == null)
            {
                return;
            }
            var userCard = (UserCard)datMyDeckCards.SelectedItem;
            loadDetailWindow(userCard);
        }

        private void datMyMatchDeckCards_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(datMyMatchDeckCards.SelectedItem == null)
            {
                return;
            }
            var userCard = (UserCard)datMyMatchDeckCards.SelectedItem;
            loadDetailWindow(userCard);
        }

        private void btnCreateDeck_Click(object sender, RoutedEventArgs e)
        {
            var deckCreationWindow = new Creation(_user, _deckManager);
            var result = deckCreationWindow.ShowDialog();
            if (result == true)
            {
                MessageBox.Show("Deck has been created.");
                datMyDecks.ItemsSource = UserDeckRetrieveHelper();
            }
        }

        private void btnUpdateDeck_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Deck deck = (Deck)datMyDecks.SelectedItem;
                var deckUpdateWindow = new Creation(_user, deck, _deckManager);
                var result = deckUpdateWindow.ShowDialog();
                if (result == true)
                {
                    MessageBox.Show("Deck has been updated.");
                    myDecksHelper();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex);
            }
        }

        private void btnDeleteDeck_Click(object sender, RoutedEventArgs e)
        {
            Deck deck = (Deck)datMyDecks.SelectedItem;
            var result = MessageBox.Show("You are about to delete " + deck.DeckName + " from your decks. Are you sure?", "Confirm", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                try
                {
                    _deckManager.RemoveDeck(deck);
                    MessageBox.Show("Deck has been removed.");
                    myDecksHelper();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("" + ex);
                }
            }
        }

        private void btnUpdateDeckCard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserCard card = (UserCard)datMyDeckCards.SelectedItem;

                DeckCard deckCard = new DeckCard()
                {
                    DeckID = _deck.DeckID,
                    CardID = card.CardID,
                    CardCount = card.CardCount,
                    CardName = card.CardName,
                    ImageID = card.ImageID,
                    CardDescription = card.CardDescription,
                    CardColorID = card.CardColorID,
                    CardConvertedManaCost = card.CardConvertedManaCost,
                    CardTypeID = card.CardTypeID,
                    CardRarityID = card.CardRarityID,
                    HasSecondaryCard = card.HasSecondaryCard,
                    CardSecondaryName = card.CardSecondaryName,
                    SecondaryImageID = card.SecondaryImageID,
                    CardSecondaryDescription = card.CardSecondaryDescription,
                    CardSecondaryColorID = card.CardSecondaryColorID,
                    CardSecondaryConvertedManaCost = card.CardSecondaryConvertedManaCost,
                    CardSecondaryTypeID = card.CardSecondaryTypeID,
                    CardSecondaryRarityID = card.CardSecondaryRarityID
                };


                var deckCardUpdateWindow = new Creation(deckCard, _deckManager);
                var result = deckCardUpdateWindow.ShowDialog();
                if (result == true)
                {
                    MessageBox.Show("Card has been updated.");
                    myDeckCardsHelper();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex);
            }
            
        }

        private void datMyDeckCards_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datMyDeckCards.SelectedItem != null)
            {
                _userCard = (UserCard)datMyDeckCards.SelectedItem;
                btnUpdateDeckCard.IsEnabled = true;
                btnDeleteDeckCard.IsEnabled = true;
            }
        }

        private void btnDeleteDeckCard_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("You are about to delete " + _userCard.CardName + " from your deck. Are you sure?", "Confirm", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                try
                {

                    UserCard card = new UserCard();
                    card = (UserCard)datMyDeckCards.SelectedItem;

                    DeckCard deckCard = new DeckCard()
                    {
                        DeckID = _deck.DeckID,
                        CardID = card.CardID,
                        CardCount = card.CardCount,
                        CardName = card.CardName,
                        ImageID = card.ImageID,
                        CardDescription = card.CardDescription,
                        CardColorID = card.CardColorID,
                        CardConvertedManaCost = card.CardConvertedManaCost,
                        CardTypeID = card.CardTypeID,
                        CardRarityID = card.CardRarityID,
                        HasSecondaryCard = card.HasSecondaryCard,
                        CardSecondaryName = card.CardSecondaryName,
                        SecondaryImageID = card.SecondaryImageID,
                        CardSecondaryDescription = card.CardSecondaryDescription,
                        CardSecondaryColorID = card.CardSecondaryColorID,
                        CardSecondaryConvertedManaCost = card.CardSecondaryConvertedManaCost,
                        CardSecondaryTypeID = card.CardSecondaryTypeID,
                        CardSecondaryRarityID = card.CardSecondaryRarityID
                    };

                    _deckManager.RemoveDeckCard(deckCard);
                    MessageBox.Show("Card has been removed");
                    myDeckCardsHelper();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex + "");
                }
            }

        }

        private void datMyDecks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datMyDecks.SelectedItem != null)
            {
                _deck = (Deck)datMyDecks.SelectedItem;
                btnUpdateDeck.IsEnabled = true;
                btnDeleteDeck.IsEnabled = true;
            }
        }

        private void btnCreateMatch_Click(object sender, RoutedEventArgs e)
        {
            var matchCreationWindow = new Creation(_user, _matchManager);
            var result = matchCreationWindow.ShowDialog();
            if (result == true)
            {
                MessageBox.Show("Match has been created.");
                myMatchesHelper();
            }
        }

        private void datMyMatches_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datMyMatches.SelectedItem != null)
            {
                _match = (Match)datMyMatches.SelectedItem;
                btnUpdateMatch.IsEnabled = true;
                btnDeleteMatch.IsEnabled = true;
            }
        }

        private void btnUpdateMatch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Match match = (Match)datMyMatches.SelectedItem;

                var matchUpdateWindow = new Creation(match, _matchManager);
                var result = matchUpdateWindow.ShowDialog();
                if (result == true)
                {
                    MessageBox.Show("Match has been updated.");
                    myMatchesHelper();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex);
            }
            
        }

        private void btnDeleteMatch_Click(object sender, RoutedEventArgs e)
        {
            Match match = (Match)datMyMatches.SelectedItem;
            var result = MessageBox.Show("You are about to delete " + match.MatchName + " from your matches. Are you sure?", "Confirm", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                try
                {
                    _matchManager.RemoveMatch(match);
                    MessageBox.Show("Match has been removed.");
                    myMatchesHelper();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("" + ex);
                }
            }
        }

        private void datMyMatchDecks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datMyMatchDecks.SelectedItem != null)
            {
                _matchDeck = (MatchDeck)datMyMatchDecks.SelectedItem;
                btnDeleteMatchDeck.IsEnabled = true;
            }
        }

        private void btnAddMatchDeck_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Deck deck = (Deck)datMyDecks.SelectedItem;

                var matchDeckUpdateWindow = new Creation(_user, deck, _matchManager);
                var result = matchDeckUpdateWindow.ShowDialog();
                if (result == true)
                {
                    MessageBox.Show("Deck has been added.");
                    myDeckCardsHelper();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex);
            }
        }

        private void btnDeleteMatchDeck_Click(object sender, RoutedEventArgs e)
        {
            MatchDeck matchDeck = (MatchDeck)datMyMatchDecks.SelectedItem;
            var result = MessageBox.Show("You are about to delete " + matchDeck.DeckName + " from this match. Are you sure?", "Confirm", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                try
                {
                    _matchManager.RemoveMatchDeck(matchDeck);
                    MessageBox.Show("Deck has been removed.");
                    myMatchDecksHelper();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }
            }
        }




        private void datCards_AutoGeneratedColumns(object sender, EventArgs e)
        {
            datCards.Columns[0].Visibility = Visibility.Collapsed;
            datCards.Columns[3].Visibility = Visibility.Collapsed;
            datCards.Columns[4].Visibility = Visibility.Collapsed;
            datCards.Columns[6].Visibility = Visibility.Collapsed;
            datCards.Columns[7].Visibility = Visibility.Collapsed;
            datCards.Columns[12].Visibility = Visibility.Collapsed;
            datCards.Columns[14].Visibility = Visibility.Collapsed;
            datCards.Columns[15].Visibility = Visibility.Collapsed;

            datCards.Columns[1].Header = "Owned";
            datCards.Columns[2].Header = "Wishlisted";
            datCards.Columns[5].Header = "Card Name";
            datCards.Columns[8].Header = "Color";
            datCards.Columns[9].Header = "Mana Cost";
            datCards.Columns[10].Header = "Type";
            datCards.Columns[11].Header = "Rarity";

            datCards.Columns[13].Header = "Secondary Card Name";
            datCards.Columns[16].Header = "Secondary Color";
            datCards.Columns[17].Header = "Secondary Mana Cost";
            datCards.Columns[18].Header = "Secondary Type";
            datCards.Columns[19].Header = "Secondary Card Rarity";
        }

        private void datDecks_AutoGeneratedColumns(object sender, EventArgs e)
        {
            datDecks.Columns[0].Visibility = Visibility.Collapsed;
            datDecks.Columns[3].Visibility = Visibility.Collapsed;

            datDecks.Columns[1].Header = "Deck Name";
            datDecks.Columns[2].Header = "Deck Creator";
        }

        private void datDeckCards_AutoGeneratedColumns(object sender, EventArgs e)
        {
            datDeckCards.Columns[0].Visibility = Visibility.Collapsed;
            datDeckCards.Columns[4].Visibility = Visibility.Collapsed;
            datDeckCards.Columns[6].Visibility = Visibility.Collapsed;
            datDeckCards.Columns[7].Visibility = Visibility.Collapsed;
            datDeckCards.Columns[12].Visibility = Visibility.Collapsed;
            datDeckCards.Columns[14].Visibility = Visibility.Collapsed;
            datDeckCards.Columns[15].Visibility = Visibility.Collapsed;

            datDeckCards.Columns[1].Header = "Owned";
            datDeckCards.Columns[2].Header = "Wishlisted";
            datDeckCards.Columns[3].Header = "Amount of Cards";
            datDeckCards.Columns[5].Header = "Card Name";
            datDeckCards.Columns[8].Header = "Color";
            datDeckCards.Columns[9].Header = "Mana Cost";
            datDeckCards.Columns[10].Header = "Type";
            datDeckCards.Columns[11].Header = "Rarity";

            datDeckCards.Columns[13].Header = "Secondary Card Name";
            datDeckCards.Columns[16].Header = "Secondary Color";
            datDeckCards.Columns[17].Header = "Secondary Mana Cost";
            datDeckCards.Columns[18].Header = "Secondary Type";
            datDeckCards.Columns[19].Header = "Secondary Card Rarity";
        }

        private void datMatches_AutoGeneratedColumns(object sender, EventArgs e)
        {
            datMatches.Columns[0].Visibility = Visibility.Collapsed;
            datMatches.Columns[3].Visibility = Visibility.Collapsed;

            datMatches.Columns[1].Header = "Match Name";
            datMatches.Columns[2].Header = "Deck Creator";
        }

        private void datMatchDecks_AutoGeneratedColumns(object sender, EventArgs e)
        {
            datMatchDecks.Columns[0].Visibility = Visibility.Collapsed;
            datMatchDecks.Columns[1].Visibility = Visibility.Collapsed;

            datMatchDecks.Columns[2].Header = "Deck Name";
            datMatchDecks.Columns[3].Header = "Public";
        }

        private void datMatchDeckCards_AutoGeneratedColumns(object sender, EventArgs e)
        {
            datMatchDeckCards.Columns[0].Visibility = Visibility.Collapsed;
            datMatchDeckCards.Columns[4].Visibility = Visibility.Collapsed;
            datMatchDeckCards.Columns[6].Visibility = Visibility.Collapsed;
            datMatchDeckCards.Columns[7].Visibility = Visibility.Collapsed;
            datMatchDeckCards.Columns[12].Visibility = Visibility.Collapsed;
            datMatchDeckCards.Columns[14].Visibility = Visibility.Collapsed;
            datMatchDeckCards.Columns[15].Visibility = Visibility.Collapsed;

            datMatchDeckCards.Columns[1].Header = "Owned";
            datMatchDeckCards.Columns[2].Header = "Wishlisted";
            datMatchDeckCards.Columns[3].Header = "Amount of Cards";
            datMatchDeckCards.Columns[5].Header = "Card Name";
            datMatchDeckCards.Columns[8].Header = "Color";
            datMatchDeckCards.Columns[9].Header = "Mana Cost";
            datMatchDeckCards.Columns[10].Header = "Type";
            datMatchDeckCards.Columns[11].Header = "Rarity";

            datMatchDeckCards.Columns[13].Header = "Secondary Card Name";
            datMatchDeckCards.Columns[16].Header = "Secondary Color";
            datMatchDeckCards.Columns[17].Header = "Secondary Mana Cost";
            datMatchDeckCards.Columns[18].Header = "Secondary Type";
            datMatchDeckCards.Columns[19].Header = "Secondary Card Rarity";
        }

        private void datMyCards_AutoGeneratedColumns(object sender, EventArgs e)
        {
            datMyCards.Columns[0].Visibility = Visibility.Collapsed;
            datMyCards.Columns[3].Visibility = Visibility.Collapsed;
            datMyCards.Columns[4].Visibility = Visibility.Collapsed;
            datMyCards.Columns[6].Visibility = Visibility.Collapsed;
            datMyCards.Columns[7].Visibility = Visibility.Collapsed;
            datMyCards.Columns[12].Visibility = Visibility.Collapsed;
            datMyCards.Columns[14].Visibility = Visibility.Collapsed;
            datMyCards.Columns[15].Visibility = Visibility.Collapsed;

            datMyCards.Columns[1].Header = "Owned";
            datMyCards.Columns[2].Header = "Wishlisted";
            datMyCards.Columns[5].Header = "Card Name";
            datMyCards.Columns[8].Header = "Color";
            datMyCards.Columns[9].Header = "Mana Cost";
            datMyCards.Columns[10].Header = "Type";
            datMyCards.Columns[11].Header = "Rarity";

            datMyCards.Columns[13].Header = "Secondary Card Name";
            datMyCards.Columns[16].Header = "Secondary Color";
            datMyCards.Columns[17].Header = "Secondary Mana Cost";
            datMyCards.Columns[18].Header = "Secondary Type";
            datMyCards.Columns[19].Header = "Secondary Card Rarity";
        }

        private void datMyDecks_AutoGeneratedColumns(object sender, EventArgs e)
        {
            datMyDecks.Columns[0].Visibility = Visibility.Collapsed;
            datMyDecks.Columns[2].Visibility = Visibility.Collapsed;

            datMyDecks.Columns[1].Header = "Deck Name";
            datMyDecks.Columns[3].Header = "Public";
        }

        private void datMyDeckCards_AutoGeneratedColumns(object sender, EventArgs e)
        {
            datMyDeckCards.Columns[0].Visibility = Visibility.Collapsed;
            datMyDeckCards.Columns[4].Visibility = Visibility.Collapsed;
            datMyDeckCards.Columns[6].Visibility = Visibility.Collapsed;
            datMyDeckCards.Columns[7].Visibility = Visibility.Collapsed;
            datMyDeckCards.Columns[12].Visibility = Visibility.Collapsed;
            datMyDeckCards.Columns[14].Visibility = Visibility.Collapsed;
            datMyDeckCards.Columns[15].Visibility = Visibility.Collapsed;

            datMyDeckCards.Columns[1].Header = "Owned";
            datMyDeckCards.Columns[2].Header = "Wishlisted";
            datMyDeckCards.Columns[3].Header = "Amount of Cards";
            datMyDeckCards.Columns[5].Header = "Card Name";
            datMyDeckCards.Columns[8].Header = "Color";
            datMyDeckCards.Columns[9].Header = "Mana Cost";
            datMyDeckCards.Columns[10].Header = "Type";
            datMyDeckCards.Columns[11].Header = "Rarity";

            datMyDeckCards.Columns[13].Header = "Secondary Card Name";
            datMyDeckCards.Columns[16].Header = "Secondary Color";
            datMyDeckCards.Columns[17].Header = "Secondary Mana Cost";
            datMyDeckCards.Columns[18].Header = "Secondary Type";
            datMyDeckCards.Columns[19].Header = "Secondary Card Rarity";
        }

        private void datMyMatches_AutoGeneratedColumns(object sender, EventArgs e)
        {
            datMyMatches.Columns[0].Visibility = Visibility.Collapsed;
            datMyMatches.Columns[2].Visibility = Visibility.Collapsed;

            datMyMatches.Columns[1].Header = "Match Name";
            datMyMatches.Columns[3].Header = "Public";
        }

        private void datMyMatchDecks_AutoGeneratedColumns(object sender, EventArgs e)
        {
            datMyMatchDecks.Columns[0].Visibility = Visibility.Collapsed;
            datMyMatchDecks.Columns[1].Visibility = Visibility.Collapsed;

            datMyMatchDecks.Columns[2].Header = "Deck Name";
            datMyMatchDecks.Columns[3].Header = "Public";
        }

        private void datMyMatchDeckCards_AutoGeneratedColumns(object sender, EventArgs e)
        {
            datMyMatchDeckCards.Columns[0].Visibility = Visibility.Collapsed;
            datMyMatchDeckCards.Columns[4].Visibility = Visibility.Collapsed;
            datMyMatchDeckCards.Columns[6].Visibility = Visibility.Collapsed;
            datMyMatchDeckCards.Columns[7].Visibility = Visibility.Collapsed;
            datMyMatchDeckCards.Columns[12].Visibility = Visibility.Collapsed;
            datMyMatchDeckCards.Columns[14].Visibility = Visibility.Collapsed;
            datMyMatchDeckCards.Columns[15].Visibility = Visibility.Collapsed;

            datMyMatchDeckCards.Columns[1].Header = "Owned";
            datMyMatchDeckCards.Columns[2].Header = "Wishlisted";
            datMyMatchDeckCards.Columns[3].Header = "Amount of Cards";
            datMyMatchDeckCards.Columns[5].Header = "Card Name";
            datMyMatchDeckCards.Columns[8].Header = "Color";
            datMyMatchDeckCards.Columns[9].Header = "Mana Cost";
            datMyMatchDeckCards.Columns[10].Header = "Type";
            datMyMatchDeckCards.Columns[11].Header = "Rarity";

            datMyMatchDeckCards.Columns[13].Header = "Secondary Card Name";
            datMyMatchDeckCards.Columns[16].Header = "Secondary Color";
            datMyMatchDeckCards.Columns[17].Header = "Secondary Mana Cost";
            datMyMatchDeckCards.Columns[18].Header = "Secondary Type";
            datMyMatchDeckCards.Columns[19].Header = "Secondary Card Rarity";
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you would like to exit the program?", "Confirm", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if(result == MessageBoxResult.OK)
            {
                this.Close();
            }
        }

        private void mnuLogout_Click(object sender, RoutedEventArgs e)
        {
            _user = null;
            updateUIForLogOut();
        }
    }
}
