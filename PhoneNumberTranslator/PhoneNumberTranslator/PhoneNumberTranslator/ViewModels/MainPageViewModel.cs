using Xamarin.Forms;
using PhoneNumberTranslator.Translator;

namespace PhoneNumberTranslator.ViewModels
{
    public class MainPageViewModel
    {
        public string PhoneNumber { get; set; }
        public string TranslatedNumber { get; set; }

        private Command translatePhoneWord;
        public Command TranslatePhoneWord
        {
            get
            {
                return translatePhoneWord ?? (translatePhoneWord = new Command(HandleTranslation));
            }
        }

        private void HandleTranslation()
        {
            TranslatedNumber = NumberTranslator.TranslateToNumber(PhoneNumber);
            MessagingCenter.Send<MainPageViewModel>(this, "NumberTranslated");
        }
    }
}
