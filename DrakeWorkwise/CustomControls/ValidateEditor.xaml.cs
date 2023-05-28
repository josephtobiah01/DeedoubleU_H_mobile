using DrakeWorkwise.Validation;

namespace DrakeWorkwise.CustomControls;

public partial class ValidateEditor : ContentView
{

    #region Fields

    ValidatableObject<string> _context = null;

    #endregion Fields





    #region Properties

    public Action<bool> EditorFocusAction = null;


    #endregion Properties





    #region Methods
    #region Constructor


    public ValidateEditor()
    {
        InitializeComponent();
        GetContext();
    }


    #endregion Constructor




    private void GetContext()
    {
        if (_context == null)
        {
            _context = (ValidatableObject<string>)this.BindingContext;
        }
    }

    private void Entry_Unfocused(object sender, FocusEventArgs e)
    {
        GetContext();
        _context.Validate();
        EditorFocusAction?.Invoke(false);

    }

    private void Entry_Focused(object sender, FocusEventArgs e)
    {
        GetContext();
        _context.ClearError();
        EditorFocusAction?.Invoke(true);

    }




    #endregion Methods





}