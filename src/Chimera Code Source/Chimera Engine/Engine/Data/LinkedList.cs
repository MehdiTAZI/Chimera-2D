#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      ________________________
//-----------------------------------------------------------------------------
#endregion
using System;

namespace Chimera.Data
{
    #region Heritance
    /// <summary>
    ///This Class Allow You Using Stack Data Structures
    /// </summary>
    public class Stack : System.Collections.Stack
    {
    }
    /// <summary>
    ///This Class Allow You Using Queue Data Structures
    /// </summary>
    public class Queue : System.Collections.Queue
    {
    }

    /// <summary>
    ///This Class Allow You Using Double Linked List Data Structures
    /// </summary>
    public class DoubleLinkedList<T> : System.Collections.Generic.LinkedList<T>
    {
    }
    #endregion
    #region SimpleLinkedListNode
    /// <summary>
    /// Node public class
    /// </summary>
    /// <typeparam name="T">Data Type</typeparam>
    public class SLLNode<T>
	{
		private T data;
		private SLLNode<T> next;
		public T Data
		{
			get{return data;}
			set{data=value;}
		}
        /// <summary>
        /// Next Data 
        /// </summary>
		public SLLNode<T> Next
		{
			get{return next;}
			set{next=value;	}
		}
        /// <summary>
        /// Constructor
        /// </summary>
        public SLLNode()
        { next = null; }
    }
    #endregion
    #region SimpleLinkedList
    /// <summary>
    /// Simple Linked List Data Structures
    /// </summary>
    public class SimpleLinkedList<T>
	{
		private SLLNode<T> head;
		private int totalNode;
            /// <summary>
            /// Total Of Nodes
            /// </summary>
        public int Count
        {
            get{ return totalNode;}
        }
		public SimpleLinkedList()
		{
			head=null;
			totalNode=0;
		}
        /// <summary>
        /// Insert Element
        /// </summary>
        /// <param name="data">Data to insert</param>
		public void Insert(T data)
		{
			if(head==null)
			{
				head=new SLLNode<T>();
				head.Data = data;
			}
			else
			{
				SLLNode<T> temp;
				temp=new SLLNode<T>();
				temp.Data=data;
				temp.Next=head;
				head=temp;
			}
			totalNode++;
		}
        /// <summary>
        /// Remove Data Which It Index Equal To The Parameter
        /// </summary>
        /// <param name="index">Index Of The Node</param>
        public void Remove(int index)
        {
            SLLNode<T> before, current;
            current =before= head;
            if (index == 0 && totalNode>0)
            {
                head = head.Next;
                System.GC.Collect();
                totalNode--;
            }
            else if (index >= 1 && index < totalNode)
            {
                for (int i = 0; i < index; i++)
                {
                    before = current;
                    current = current.Next;
                }
                before.Next = current.Next;
                System.GC.Collect(); 
                totalNode--;
            }
        }
        /// <summary>
        /// Remove All Nodes Which Equal To The Parameter And Retoun Number of deleted one
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
		public int RemoveAll(T data)
		{
			SLLNode<T> before,current;
			int total=0;
			before=null;
			current=head;
			while(current!=null)
			{
				if(current.Data .Equals(data))
				{
					total++;
					totalNode--;
					if(before==null)
					{
						head=current.Next;
						System.GC.Collect();  
					}
					else
					{
						before.Next=current.Next;
						System.GC.Collect(); 
					}
				}
				before=current;
				current=current.Next; 
			}
			return total;
		}
        /// <summary>
        /// Get Or Set Data's Node indicated by the index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
		public T this [int index]
		{
			get
			{	
				SLLNode<T> temp=head;
				
                if (index >= 0 && index < totalNode)
				{
					for(int i=0;i<index;i++)
					{
						temp=temp.Next; 
					}
					return temp.Data; 
				}
				else
				{
                    throw (new IndexOutOfRangeException()); //Leve une exeption Index Hors Les Bornes du Tableau
				}
			}
			set
			{
				SLLNode<T> temp=head;
				int i;
                if (index >= 0 && index < totalNode)
				{
					for(i=0;i<index;i++)
					{
						temp=temp.Next; 
					}
				    temp.Data=value; 
				}
				else
				{
					throw(new IndexOutOfRangeException()); //Leve une exeption Index Hors Les Bornes du Tableau
				}
			}
		}
        /// <summary>
        /// Traverse ALL the nodes and execute a func
        /// </summary>
        /// <param name="func"></param>
		public void Traverse(Chimera.Helpers.Delegate.Param_Method func)
		{
			SLLNode<T> temp=head;
			while(temp!=null)
			{
                func(temp.Data);
				temp=temp.Next; 
			}
		}
    }
    #endregion
}
