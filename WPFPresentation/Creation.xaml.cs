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
using System.Windows.Shapes;
using DataAccessFakes;
using DataAccessInterfaces;
using DataObjects;
using LogicLayer;

namespace WPFPresentation
{
    /// <summary>
    /// Interaction logic for Creation.xaml
    /// </summary>
    public partial class Creation : Window
    {
        UserCard _card;
        DeckCard _deckCard;
        User _user;
        Deck _deck;
        List<Deck> _userDecks;
        //MatchDeck _matchDeck;
        Match _match;
        List<Match> _userMatches;
        IDeckManager _deckManager;
        IMatchManager _matchManager;

        public Creation(User user, IDeckManager deckManager)
        {
            _user = user;
            _deckManager = deckManager;
            InitializeComponent();
            populateControls();
        }

        public Creation(User user, Deck deck, IDeckManager deckManager)
        {
            _user = user;
            _deck = deck;
            _deckManager = deckManager;
            InitializeComponent();
            populateControls();
        }

        public Creation(UserCard card, IDeckManager deckManager)
        {
            _card = card;
            _deckManager = deckManager;
            InitializeComponent();
            populateControls();
        }

        public Creation(DeckCard card, IDeckManager deckManager)
        {
            _deckCard = card;
            _deckManager = deckManager;
            InitializeComponent();
            populateControls();
        }

        public Creation(User user, IMatchManager matchManager)
        {
            _user = user;
            _matchManager = matchManager;
            InitializeComponent();
            populateControls();
        }

        public Creation(Match match, IMatchManager matchManager)
        {
            _match = match;
            _matchManager = matchManager;
            InitializeComponent();
            populateControls();
        }

        public Creation(User user, Deck deck, IMatchManager matchManager)
        {
            _user = user;
            _deck = deck;
            _matchManager = matchManager;
            InitializeComponent();
            populateControls();
        }

        private void populateControls()
        {
            if (_user == null && _deckCard == null && _matchManager == null)
            {
                grdDeckCardCreation.Visibility = Visibility.Visible;
                grdDeckCreation.Visibility = Visibility.Collapsed;
                grdDeckUpdate.Visibility = Visibility.Collapsed;
                grdDeckCardUpdate.Visibility = Visibility.Collapsed;
                grdMatchCreation.Visibility = Visibility.Collapsed;
                grdMatchUpdate.Visibility = Visibility.Collapsed;
                grdMatchDeckCreation.Visibility = Visibility.Collapsed;
                try
                {
                    _userDecks = _deckManager.RetrieveUserDecksByUserID(_card.UserID);
                    cboDeckNames.ItemsSource = from m in _userDecks
                                               orderby m.DeckName
                                               select m.DeckName;
                }
                catch (Exception)
                {

                    MessageBox.Show("User decks not retrieved.");
                }
                cboDeckNames.Focus();
            }
            else if (_user == null && _matchManager == null)
            {
                grdDeckCardUpdate.Visibility = Visibility.Visible;
                grdDeckCardCreation.Visibility = Visibility.Collapsed;
                grdDeckCreation.Visibility = Visibility.Collapsed;
                grdDeckUpdate.Visibility = Visibility.Collapsed;
                grdMatchCreation.Visibility = Visibility.Collapsed;
                grdMatchUpdate.Visibility = Visibility.Collapsed;
                grdMatchDeckCreation.Visibility = Visibility.Collapsed;
                txtNewCardAmount.Focus();
            }
            else if (_deck == null && _card == null && _matchManager == null) 
            {
                grdDeckCreation.Visibility = Visibility.Visible;
                grdDeckUpdate.Visibility = Visibility.Collapsed;
                grdDeckCardCreation.Visibility = Visibility.Collapsed;
                grdDeckCardUpdate.Visibility = Visibility.Collapsed;
                grdMatchCreation.Visibility = Visibility.Collapsed;
                grdMatchUpdate.Visibility = Visibility.Collapsed;
                grdMatchDeckCreation.Visibility = Visibility.Collapsed;
                txtDeckName.Focus();
            }
            else if (_deck != null && _matchManager == null)
            {
                grdDeckUpdate.Visibility = Visibility.Visible;
                grdDeckCreation.Visibility = Visibility.Collapsed;
                grdDeckCardCreation.Visibility = Visibility.Collapsed;
                grdDeckCardUpdate.Visibility = Visibility.Collapsed;
                grdMatchCreation.Visibility = Visibility.Collapsed;
                grdMatchUpdate.Visibility = Visibility.Collapsed;
                grdMatchDeckCreation.Visibility = Visibility.Collapsed;
                txtNewDeckName.Text = _deck.DeckName;
                chkNewDeckPublic.IsChecked = _deck.IsPublic;
                txtNewDeckName.Focus();
            }
            else if(_deck == null && _match == null)
            {
                grdMatchCreation.Visibility = Visibility.Visible;
                grdDeckUpdate.Visibility = Visibility.Collapsed;
                grdDeckCreation.Visibility = Visibility.Collapsed;
                grdDeckCardCreation.Visibility = Visibility.Collapsed;
                grdDeckCardUpdate.Visibility = Visibility.Collapsed;
                grdMatchUpdate.Visibility = Visibility.Collapsed;
                grdMatchDeckCreation.Visibility = Visibility.Collapsed;
                txtMatchName.Focus();
            }
            else if(_deck == null)
            {
                grdMatchUpdate.Visibility = Visibility.Visible;
                grdMatchCreation.Visibility = Visibility.Collapsed;
                grdDeckUpdate.Visibility = Visibility.Collapsed;
                grdDeckCreation.Visibility = Visibility.Collapsed;
                grdDeckCardCreation.Visibility = Visibility.Collapsed;
                grdDeckCardUpdate.Visibility = Visibility.Collapsed;
                grdMatchDeckCreation.Visibility = Visibility.Collapsed;
                txtNewMatchName.Text = _match.MatchName;
                chkNewMatchPublic.IsChecked = _match.IsPublic;
                txtNewMatchName.Focus();
            }
            else if(_deckManager == null)
            {
                grdMatchDeckCreation.Visibility = Visibility.Visible;
                grdMatchUpdate.Visibility = Visibility.Collapsed;
                grdMatchCreation.Visibility = Visibility.Collapsed;
                grdDeckUpdate.Visibility = Visibility.Collapsed;
                grdDeckCreation.Visibility = Visibility.Collapsed;
                grdDeckCardCreation.Visibility = Visibility.Collapsed;
                grdDeckCardUpdate.Visibility = Visibility.Collapsed;

                try
                {
                    _userMatches = _matchManager.RetrieveUserMatchesByUserID(_user.UserID);
                    cboMatchNames.ItemsSource = from m in _userMatches
                                               orderby m.MatchName
                                               select m.MatchName;
                }
                catch (Exception)
                {

                    MessageBox.Show("User decks not retrieved.");
                }

                cboMatchNames.Focus();
            }
        }

        private void btnDeckSave_Click(object sender, RoutedEventArgs e)
        {
            if(txtDeckName.Text == "")
            {
                MessageBox.Show("Please fill out the deck name.");
                txtDeckName.Focus();
                return;
            }
            if(txtDeckName.Text.Length > 50)
            {
                MessageBox.Show("Deck name cannot be greater than 50 characters");
                txtDeckName.Focus();
                return;
            }

            string deckName = txtDeckName.Text.ToString();
            bool isPublic = (bool)chkDeckPublic.IsChecked;
            try
            {
                _deckManager.CreateDeck(deckName, _user.UserID, isPublic);
                DialogResult = true;
                this.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cancelHelper()
        {
            DialogResult = false;
            this.Close();
        }

        private void btnDeckCancel_Click(object sender, RoutedEventArgs e)
        {
            cancelHelper();
        }

        private void btnNewDeckUpdate_Click(object sender, RoutedEventArgs e)
        {
            Deck newDeck = new Deck() {
                DeckID = _deck.DeckID,
                DeckName = txtNewDeckName.Text.ToString(),
                UserID = _deck.UserID,
                IsPublic = (bool)chkNewDeckPublic.IsChecked
            };
            if (txtNewDeckName.Text == "")
            {
                MessageBox.Show("Please fill out the deck name.");
                txtDeckName.Focus();
                return;
            }
            if (txtNewDeckName.Text.Length > 50)
            {
                MessageBox.Show("Deck name cannot be greater than 50 characters");
                txtDeckName.Focus();
                return;
            }

            string deckName = txtNewDeckName.Text.ToString();
            bool isPublic = (bool)chkDeckPublic.IsChecked;
            try
            {
                _deckManager.EditDeck(_deck, newDeck);
                DialogResult = true;
                this.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnNewDeckCancel_Click(object sender, RoutedEventArgs e)
        {
            cancelHelper();
        }

        private void btnNewDeckCardSave_Click(object sender, RoutedEventArgs e)
        {
            int amount = 0;
            if(cboDeckNames.SelectedItem == null)
            {
                MessageBox.Show("You must select a deck name.");
                cboDeckNames.Focus();
                return;
            }
            if (txtCardAmount.Text == "")
            {
                MessageBox.Show("You must enter a card amount.");
                txtCardAmount.Focus();
                return;
            }
            try
            {
                amount = Int32.Parse(txtCardAmount.Text.Trim());
            }
            catch (Exception)
            {
                MessageBox.Show("Card Amount must be a number.");
                txtCardAmount.Focus();
                return;
            }
            int deckID = _userDecks.First(m => m.DeckName == cboDeckNames.Text.ToString()).DeckID;
            DeckCard card = new DeckCard()
            {
                DeckID = deckID,
                CardID = _card.CardID,
                CardCount = amount,
                CardName = _card.CardName,
                ImageID = _card.ImageID,
                CardDescription = _card.CardDescription,
                CardColorID = _card.CardColorID,
                CardConvertedManaCost = _card.CardConvertedManaCost,
                CardTypeID = _card.CardTypeID,
                CardRarityID = _card.CardRarityID,
                HasSecondaryCard = _card.HasSecondaryCard,
                CardSecondaryName = _card.CardSecondaryName,
                SecondaryImageID = _card.SecondaryImageID,
                CardSecondaryDescription = _card.CardSecondaryDescription,
                CardSecondaryColorID = _card.CardSecondaryColorID,
                CardSecondaryConvertedManaCost = _card.CardSecondaryConvertedManaCost,
                CardSecondaryTypeID = _card.CardSecondaryTypeID,
                CardSecondaryRarityID = _card.CardSecondaryRarityID
            };
            try
            {
                _deckManager.CreateDeckCard(card);
                DialogResult = true;
                this.Close();
            }
            catch (Exception)
            {

                MessageBox.Show("Card is already in this deck");
            }
        }

        private void btnNewDeckCardCancel_Click(object sender, RoutedEventArgs e)
        {
            cancelHelper();
        }

        private void btnUpdateDeckCardSave_Click(object sender, RoutedEventArgs e)
        {
            int amount = 0;
            if (txtNewCardAmount.Text == "")
            {
                MessageBox.Show("You must enter a card amount.");
                txtNewCardAmount.Focus();
                return;
            }
            try
            {
                amount = Int32.Parse(txtNewCardAmount.Text.Trim());
            }
            catch (Exception)
            {
                MessageBox.Show("Card Amount must be a number.");
                txtNewCardAmount.Focus();
                return;
            }

            DeckCard newDeckCard = new DeckCard()
            {
                DeckID = _deckCard.DeckID,
                CardID = _deckCard.CardID,
                CardCount = amount,
                CardName = _deckCard.CardName,
                ImageID = _deckCard.ImageID,
                CardDescription = _deckCard.CardDescription,
                CardColorID = _deckCard.CardColorID,
                CardConvertedManaCost = _deckCard.CardConvertedManaCost,
                CardTypeID = _deckCard.CardTypeID,
                CardRarityID = _deckCard.CardRarityID,
                HasSecondaryCard = _deckCard.HasSecondaryCard,
                CardSecondaryName = _deckCard.CardSecondaryName,
                SecondaryImageID = _deckCard.SecondaryImageID,
                CardSecondaryDescription = _deckCard.CardSecondaryDescription,
                CardSecondaryColorID = _deckCard.CardSecondaryColorID,
                CardSecondaryConvertedManaCost = _deckCard.CardSecondaryConvertedManaCost,
                CardSecondaryTypeID = _deckCard.CardSecondaryTypeID,
                CardSecondaryRarityID = _deckCard.CardSecondaryRarityID
            };

            try
            {
                _deckManager.EditDeckCard(_deckCard, newDeckCard);
                DialogResult = true;
                this.Close();
            }
            catch (Exception)
            {

                MessageBox.Show("Error.");
            }
        }

        private void btnUpdateDeckCardCancel_Click(object sender, RoutedEventArgs e)
        {
            cancelHelper();
        }

        private void btnMatchSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtMatchName.Text == "")
            {
                MessageBox.Show("Please fill out the match name.");
                txtMatchName.Focus();
                return;
            }
            if (txtMatchName.Text.Length > 100)
            {
                MessageBox.Show("Match name cannot be greater than 100 characters");
                txtMatchName.Focus();
                return;
            }

            string matchName = txtMatchName.Text.ToString();
            bool isPublic = (bool)chkMatchPublic.IsChecked;
            try
            {
                _matchManager.CreateMatch(matchName, _user.UserID, isPublic);
                DialogResult = true;
                this.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnMatchCancel_Click(object sender, RoutedEventArgs e)
        {
            cancelHelper();
        }

        private void btnNewMatchUpdate_Click(object sender, RoutedEventArgs e)
        {
            Match newMatch = new Match()
            {
                MatchID = _match.MatchID,
                MatchName = txtNewMatchName.Text.ToString(),
                UserID = _match.UserID,
                IsPublic = (bool)chkNewMatchPublic.IsChecked
            };
            if (txtNewMatchName.Text == "")
            {
                MessageBox.Show("Please fill out the match name.");
                txtNewMatchName.Focus();
                return;
            }
            if (txtNewMatchName.Text.Length > 100)
            {
                MessageBox.Show("Match name cannot be greater than 100 characters");
                txtNewMatchName.Focus();
                return;
            }

            string matchName = txtNewMatchName.Text.ToString();
            bool isPublic = (bool)chkNewMatchPublic.IsChecked;
            try
            {
                _matchManager.EditMatch(_match, newMatch);
                DialogResult = true;
                this.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnNewMatchCancel_Click(object sender, RoutedEventArgs e)
        {
            cancelHelper();
        }

        private void btnMatchDeckSave_Click(object sender, RoutedEventArgs e)
        {
            bool winner = (bool)chkMatchDeckWinner.IsChecked;
            if (cboMatchNames.SelectedItem == null)
            {
                MessageBox.Show("You must select a match name.");
                cboMatchNames.Focus();
                return;
            }
            int matchID = _userMatches.First(m => m.MatchName == cboMatchNames.Text.ToString()).MatchID;
            MatchDeck matchDeck = new MatchDeck()
            {
                MatchID = matchID,
                DeckID = _deck.DeckID,
                DeckName = _deck.DeckName,
                Winner = winner
            };
            try
            {
                _matchManager.CreateMatchDeck(matchDeck);
                DialogResult = true;
                this.Close();
            }
            catch (Exception)
            {

                MessageBox.Show("Deck is already in this match");
            }
        }

        private void btnMatchDeckCancel_Click(object sender, RoutedEventArgs e)
        {
            cancelHelper();
        }
    }
}
