using Microsoft.Extensions.Configuration;
using RepositoryLayer.AppContext;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class LabelRL : ILabelRL
    {
        Context context;
        private readonly IConfiguration configuration;
        public LabelRL(Context context, IConfiguration config)
        {
            this.context = context;//appcontext to for api
            this.configuration = config;//for startup file instance
        }


        public bool CreateLabel(long Id, long NoteId, string newlabelName)
        {
            try
            {
                Label labels = new Label();
                var findlabel = context.Labels.Where(e => e.LabelName == newlabelName).FirstOrDefault();
                if (findlabel == null)
                {
                    labels.LabelName = newlabelName;
                    labels.NoteId = NoteId;
                    labels.Id = Id;

                    context.Labels.Add(labels);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {

                throw;
            }

        }



        public IEnumerable<Label> RenameLabel(long Id, string oldLabelName, string newlabelName)
        {
            IEnumerable<Label> labels;
            labels = context.Labels.Where(e => e.Id == Id && e.LabelName == oldLabelName).ToList();

            if (labels != null)
            {
                foreach (var label in labels)
                {
                    label.LabelName = newlabelName;
                }
                context.SaveChanges();
                return labels;
            }

            else
            {
                return null;
            }

        }

        public IEnumerable<Label> GetLabelByNoteId(long Id, long NoteId)
        {
            try
            {
                var result = context.Labels.Where(e => e.NoteId == NoteId && e.Id == Id).ToList();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteLabel(long Id, string labelName)
        {


            IEnumerable<Label> labels;

            labels = context.Labels.Where(e => e.Id == Id && e.LabelName == labelName).ToList();
            if (labels != null)
            {
                foreach (var label in labels)
                {
                    context.Labels.Remove(label);
                }
                context.SaveChanges();
                return true;
            }

            else
            {
                return false;
            }
        }


        public bool DeleteLabelByNoteId(long Id, long NoteId, string labelName)
        {
            var label = context.Labels.Where(e => e.Id == Id && e.LabelName == labelName && e.NoteId == NoteId).FirstOrDefault();
            if (label != null)
            {
                context.Labels.Remove(label);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}