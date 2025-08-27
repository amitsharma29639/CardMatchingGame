using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    private Stack<UIScreen> screenStack = new Stack<UIScreen>();
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Ensure only one exists
            return;
        }

        Instance = this;
    }

    /// <summary>
    /// Show a new screen and push it on the stack
    /// </summary>
    public void PushScreen(UIScreen screen)
    {
        // if (screenStack.Count > 0)
        // {
        //     // Optionally hide current screen
        //     screenStack.Peek().Hide();
        // }

        screenStack.Push(screen);
        screen.Show();
    }

    /// <summary>
    /// Hide the current screen and pop it from the stack
    /// </summary>
    public void PopScreen()
    {
        if (screenStack.Count == 0) return;

        UIScreen top = screenStack.Pop();
        top.Hide();

        if (screenStack.Count > 0)
        {
            screenStack.Peek().Show();
        }
    }

    /// <summary>
    /// Handle back press
    /// </summary>
    public void HandleBackPress()
    {
        if (screenStack.Count == 0) return;

        UIScreen top = screenStack.Peek();
        bool handled = top.OnBackPressed();

        if (!handled)
        {
            PopScreen();
        }
    }
}