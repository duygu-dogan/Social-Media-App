using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Application.Follows.Commands.DeleteFollow;
public record DeleteFollowCommand(string id): IRequest
{ 
}
