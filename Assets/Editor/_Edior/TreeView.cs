using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

class TreeView : EditorWindow {
    // [UnityEngine.SerializeField]
    // // The TreeElement data class is extended to hold extra data, which you show and edit in the
    // // front-end TreeView.
    // internal class SimpleTreeElement : TreeViewItem {
    //     public float floatValue1, floatValue2, floatValue3;
    //     public Material material;
    //     public string text = "";
    //     public bool enable = true;

    //     public SimpleTreeElement(int id, int depth, string name) : base(id, depth, name) {
    //         floatValue1 = Random.value;
    //         floatValue2 = Random.value;
    //         floatValue3 = Random.value;
    //     }
    // }

    // [CreateAssetMenu(fileName = "TreeDataAsset", menuName = "Tree Asset", order = 1)]
    // public class SimpleTreeAsset : ScriptableObject {
    //     [SerializeField]
    //     List<SimpleTreeElement> m_TreeElements = new List<SimpleTreeElement>();

    //     internal List<SimpleTreeElement> treeElements {
    //         get { return m_TreeElements; }
    //         set { m_TreeElements = value; }
    //     }
    // }

    class SimpleTreeView : UnityEditor.IMGUI.Controls.TreeView {
        public SimpleTreeView(TreeViewState treeViewState) : base(treeViewState) {
            Reload();
        }

        protected override TreeViewItem BuildRoot() {
            // BuildRoot is called every time Reload is called to ensure that TreeViewItems
            // are created from data. Here we create a fixed set of items, In a real world example
            // a data model should be passed into the TreeView and the items created from the model

            // this section illustrates that IDs should be unique. The root item is required to
            // have a depth of -1, and rest of the items increment from that.
            var root = new TreeViewItem { id = -1, depth = -1, displayName = "Root" };
            var allItems = new List<TreeViewItem> {
                new TreeViewItem { id = 0, depth = 0, displayName = "0" },
                new TreeViewItem { id = 1, depth = 1, displayName = "1" },
                new TreeViewItem { id = 2, depth = 2, displayName = "2" },
                new TreeViewItem { id = 3, depth = 3, displayName = "3" },
                new TreeViewItem { id = 4, depth = 4, displayName = "4" },
                new TreeViewItem { id = 5, depth = 5, displayName = "5" },
                new TreeViewItem { id = 6, depth = 6, displayName = "6" },
            };

            // Utility method that initializes the TreeViewItem.chindren and .parent for all items
            SetupParentsAndChildrenFromDepths(root, allItems);

            // Return root of the tree
            return root;
        }
    }

    // SerializeField is used to ensure the view state is written to the window
    // layout file. This means that the state survives restarting Unity as long as the window
    // is not closed. If the attribute is omitted then the state is still serialized/deserialized
    [UnityEngine.SerializeField]
    TreeViewState m_TreeViewState;

    // The TreeView is not serialized, so it should be reconstructed from the tree date
    SimpleTreeView m_SimpleTreeView;

    void OnEnable() {
        // Check whether there is already a serialized view state (state that survived assembly)
        // reloading
        if (m_TreeViewState == null) m_TreeViewState = new TreeViewState();

        m_SimpleTreeView = new SimpleTreeView(m_TreeViewState);
    }

    void OnGUI() {
        m_SimpleTreeView.OnGUI(new UnityEngine.Rect(0, 0, position.width, position.height));
    }

    // add menu named "Tree View" to the jsky menu
    [UnityEditor.MenuItem("Tools/Jsky/Tree View")]
    static void ShowWindow() {
        // Get existing open window or if none make a new one:
        var window = GetWindow<TreeView>();
        window.titleContent = new UnityEngine.GUIContent("Tree View");
        window.Show();
    }
}