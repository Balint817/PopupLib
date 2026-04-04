using LocalizeLib;
using PopupLib.Patches.RefreshBulletinFix;
using System;
using System.Collections.Generic;
using UnityEngine;
using BulletinDataModel = Il2CppAssets.Scripts.UI.Panels.Bulletin.PnlStageBulletinDataModel;

namespace PopupLib.UI.Components
{
    /// <summary>
    /// Class used to store data info relevant to forum windows
    /// </summary>
    public class ForumObject
    {
        public ForumObject Copy(bool deepCopyTexture = false)
        {
            var result = new ForumObject(_title?.Copy()!, _contents?.Copy()!, IsNew);
            if (!deepCopyTexture || Texture is null)
            {
                result.Texture = Texture;
                return result;
            }
            var texture = new Texture2D(Texture.width, Texture.height);
            texture.SetPixels(Texture.GetPixels());
            texture.Apply();
            result.Texture = texture;
            return result;
        }
        private LocalString _title = null!;
        private LocalString _contents = null!;
        public LocalString Titles
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value ?? new LocalString();
            }
        }
        public LocalString Contents
        {
            get
            {
                return _contents;
            }
            set
            {
                _contents = value ?? new LocalString();
            }
        }

        public bool IsNew;
        public Texture2D? Texture;
        public string? TextureURL;
        public ForumObject(LocalString titles, LocalString contents, bool isNew = false)
        {
            Titles = titles;
            Contents = contents;
            IsNew = isNew;
        }
        //public ForumObject(LocalString titles, LocalString contents, List<Action> linkFunctions)
        //{
        //    Titles = titles ?? new LocalString();
        //    Contents = contents ?? new LocalString();
        //    this.linkFunctions = linkFunctions;
        //}

        internal IEnumerable<Tuple<string, BulletinDataModel>> GetBulletins(int idx)
        {
            foreach (var item in LocalString.GetContents(Titles, Contents))
            {
                yield return new Tuple<string, BulletinDataModel>(item[0]!, new BulletinDataModel()
                {
                    title = (item[1] ?? ""),
                    content = (item[2] ?? ""),
                    isNew = IsNew,
                    //texture = Texture ?? ModMain.NullTexture,
                    imageUrl = TextureURL ?? (BulletinLoadTexturePatch.PopupLibURLPrefix + idx),
                    uid = idx.ToString(),
                    force = true
                });
            }
            ;
        }
    }
}
