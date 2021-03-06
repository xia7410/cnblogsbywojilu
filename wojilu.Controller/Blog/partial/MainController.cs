/*
 * Copyright (c) 2010, www.wojilu.com. All rights reserved.
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using wojilu.Web.Mvc;
using wojilu.Members.Users.Domain;
using wojilu.Apps.Blog.Domain;
using wojilu.Common.Resource;
using System.Text.RegularExpressions;

namespace wojilu.Web.Controller.Blog {

    public partial class MainController : ControllerBase {

        private void bindTechUsers(IList userRanks)
        {

            int i = 0;
            IBlock block = getBlock("techusers");
            foreach (User user in userRanks)
            {
                block.Set("user.Name", user.Name);
                block.Set("user.No", ++i);
                block.Set("user.Link", Link.ToMember(user));
                block.Next();
            }

        }
        private void bindHumanityUsers(IList userRanks)
        {

            int i = 0;
            IBlock block = getBlock("humanityusers");
            foreach (User user in userRanks)
            {
                block.Set("user.Name", user.Name);
                block.Set("user.No", ++i);
                block.Set("user.Link", Link.ToMember(user));
                block.Next();
            }

        }
        private void bindUsers( IList userRanks ) {

            int i = 0;
            IBlock block = getBlock( "users" );
            foreach (User user in userRanks) {
                block.Set( "user.Name", user.Name );
                block.Set("user.No", ++i);
                block.Set( "user.Link", Link.ToMember( user ) );
                block.Next();
            }

        }

        private int i = 1;

        private void bindSidebar( IList tops, IList hits, IList replies ) {
            i = 1;

            bindList( "tops", "post", tops, bindLink );

            i = 1;
            bindList( "hits", "post", hits, bindLink );

            i = 1;
            bindList( "replies", "post", replies, bindLink );
        }

        private void bindLink( IBlock tpl, String lbl, object obj ) {

            BlogPost post = obj as BlogPost;
            String userLink = Link.ToUser( post.CreatorUrl );

            String userFace = "";
            if (strUtil.HasText( post.Creator.Pic )) {
                //userFace = string.Format( "<a href='{0}'><img src='{1}'/></a><br/>", userLink, post.Creator.PicSmall );
                tpl.Set("post.Face", post.Creator.PicSmall);
            }
            //userFace += string.Format( "<a href='{0}'>{1}</a>", userLink, post.Creator.Name );
            String title = Regex.Replace(post.Title, key, "<font color=\"red\">" + key + "</font>", RegexOptions.IgnoreCase);

            String abs = strUtil.HasText( post.Abstract ) ? post.Abstract : strUtil.ParseHtml( post.Content, 150 );
            abs = Regex.Replace(abs, key, "<font color=\"red\">" + key + "</font>", RegexOptions.IgnoreCase);

            //tpl.Set( "post.Face", userFace );
            tpl.Set("post.Title", title);
            tpl.Set( "post.Abstract", abs );
            tpl.Set( "post.LinkShow", alink.ToAppData( post ) );
            tpl.Set("post.userLink", userLink);
            tpl.Set("post.Creator", post.Creator.Name);
            tpl.Set("viewLink", alink.ToAppData(post));
            tpl.Set("post.Hits", post.Hits);
            tpl.Set("commentLink", alink.ToAppData(post) + "#reply");
            tpl.Set("post.ReplyCount", post.Replies);

            tpl.Set( "post.Number", i );

            i++;
        }


        private String getDropList( int val ) {
            PropertyCollection plist = new PropertyCollection();
            plist.Add( new PropertyItem( lang( "title" ), 1 ) );
            plist.Add( new PropertyItem( lang( "author" ), 2 ) );

            return Html.DropList( plist, "qtype", "Name", "Value", val );
        }

    }

}
