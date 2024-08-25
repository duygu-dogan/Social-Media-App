using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Application.Likes.Commands.DeleteLike;
public record DeleteLikeCommand(string? id): IRequest;
