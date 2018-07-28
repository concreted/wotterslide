# Git Workflow

**Git** is a **version control** system that lets many people work on the same project without interfering with others' work, and preserves history in case you want to undo a change.

Normally when you save work in Photoshop or Word, you are overwriting the only copy of that file. When using Git, instead you:
1. Start from the **master** version (we call it the master branch) of the project. This is the "canonical" or "definitive" version of the project that everyone works off of.
1. Tell Git you want to make some changes (create a branch from master)
1. Make your changes like you would before, i.e. open Photoshop/Word/Unity/whatever program and make/save changes.
1. Tell Git you have changes you want to save to version control. (`git add`)
1. Commit those changes to version control (`git commit`)

The power of Git is that you're no longer working off of a single file or project - you're working off of **versions** of that project. No matter what changes you make, you can always roll back to an old version saved in Git.

When you have a change you're happy with, you submit a request to add it to the **master** branch. This is called "making a pull request".

## Starting new work (i.e. working from a new branch)
1. Open Terminal
1. `cd ~/src/wotterslide`
    - This moves you into the project directory.
1. `git checkout master && git pull`
    - This switches to the master branch and updates your local copy of the project to the latest version.
1. `git checkout -b <new_branch_name>`
    - Replace `<new_branch_name>` with a unique name for your new branch.
    - Don't include the angle brackets `<>`.
    - An example is `git checkout -b aric/feat/more_enemies`
    - After running, you should see a message `Switched to a new branch '<new_branch_name'`.
1. Open Unity. Open the project at `~/src/wotterslide`.
1. Do your work in Unity. Make sure to save inside Unity when you complete work.
1. After you make your changes, go back to the Terminal window you opened.
1. `git add .`
    - This tells Git that the changes you made should be "committed" to Git's version control.
1. `git commit -m "<commit_description>"`
    - Replace `<commit_description>` with a description of what your change is.
    - Example: `git commit -m "Add more enemies"`
    - This **commits** your change to Git and includes the description, so people know what your change does.

## Making a Pull Request

TODO, please ask Aric or Eli for help
