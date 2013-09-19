using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Text.RegularExpressions;


namespace ChART.Domain.Entities
{
    public class Station
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("location")]
        public double[] Location { get; set; }
        [JsonProperty("latitute")]
        public double Latitude { get {return Location[1];} set{ Location[1] = value; } }
        [JsonProperty("longitude")]
        public double Longitude { get{return Location[0];} set{ Location[0] = value; } }
        [JsonProperty("route")]
        public string Route { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }

        public Station()
        {
            Location = new double[2];
        }

		public PointF LocationPoint
		{ 
			get{
				return new PointF ((float)Longitude, (float)Latitude);
			} 
		}

		public static readonly PointF TroncalRouteCenter = new PointF (-106.07476f, 28.639196f);
		public static readonly PointF[] TroncalRoutePath = {
			new PointF (-106.124733f,28.703550f),
			new PointF (-106.132629f,28.711241f),
			new PointF (-106.132820f,28.711109f),
			new PointF (-106.125092f,28.703430f),
			new PointF (-106.110603f,28.688070f),
			new PointF (-106.110458f,28.687759f),
			new PointF (-106.110413f,28.687401f),
			new PointF (-106.110481f,28.687040f),
			new PointF (-106.111198f,28.685289f),
			new PointF (-106.111259f,28.684839f),
			new PointF (-106.111237f,28.684500f),
			new PointF (-106.111053f,28.683910f),
			new PointF (-106.110718f,28.683430f),
			new PointF (-106.104713f,28.678240f),
			new PointF (-106.097122f,28.661961f),
			new PointF (-106.096703f,28.661280f),
			new PointF (-106.096024f,28.660580f),
			new PointF (-106.086197f,28.651489f),
			new PointF (-106.085953f,28.651230f),
			new PointF (-106.085953f,28.651051f),
			new PointF (-106.085861f,28.650961f),
			new PointF (-106.085617f,28.650961f),
			new PointF (-106.077065f,28.643084f),
			new PointF (-106.076393f,28.642481f),
			new PointF (-106.073891f,28.640120f),
			new PointF (-106.075447f,28.638540f),
			new PointF (-106.076767f,28.637341f),
			new PointF (-106.077171f,28.637079f),
			new PointF (-106.079437f,28.635290f),
			new PointF (-106.079651f,28.635450f),
			new PointF (-106.079422f,28.635590f),
			new PointF (-106.075798f,28.632490f),
			new PointF (-106.075661f,28.632299f),
			new PointF (-106.074982f,28.629829f),
			new PointF (-106.073860f,28.628031f),
			new PointF (-106.073601f,28.627750f),
			new PointF (-106.073013f,28.627380f),
			new PointF (-106.062439f,28.621380f),
			new PointF (-106.058495f,28.619190f),
			new PointF (-106.058167f,28.619068f),
			new PointF (-106.057915f,28.619036f),
			new PointF (-106.057632f,28.619057f),
			new PointF (-106.057358f,28.619131f),
			new PointF (-106.056091f,28.619671f),
			new PointF (-106.054764f,28.620209f),
			new PointF (-106.053818f,28.620613f),
			new PointF (-106.052849f,28.621016f),
			new PointF (-106.050896f,28.621763f),
			new PointF (-106.050407f,28.621944f),
			new PointF (-106.050156f,28.622040f),
			new PointF (-106.049927f,28.622091f),
			new PointF (-106.049767f,28.622116f),
			new PointF (-106.049576f,28.622141f),
			new PointF (-106.048340f,28.622156f),
			new PointF (-106.047058f,28.622156f),
			new PointF (-106.046684f,28.622150f),
			new PointF (-106.046310f,28.622177f),
			new PointF (-106.043434f,28.622725f),
			new PointF (-106.042862f,28.622772f),
			new PointF (-106.042542f,28.622755f),
			new PointF (-106.042236f,28.622715f),
			new PointF (-106.037262f,28.621937f),
			new PointF (-106.031799f,28.621040f),
			new PointF (-106.028389f,28.620461f),
			new PointF (-106.022881f,28.619440f),
			new PointF (-106.019989f,28.618990f),
			new PointF (-106.019691f,28.619020f),
			new PointF (-106.019592f,28.619209f),
			new PointF (-106.027184f,28.624420f),
			new PointF (-106.027428f,28.624100f),
			new PointF (-106.027397f,28.623960f),
			new PointF (-106.027451f,28.623871f),
			new PointF (-106.027611f,28.623819f),
			new PointF (-106.028069f,28.623899f),
			new PointF (-106.028687f,28.624331f),
			new PointF (-106.028397f,28.624670f),
			new PointF (-106.027847f,28.624260f),
			new PointF (-106.027618f,28.624260f),
			new PointF (-106.026978f,28.623840f),
			new PointF (-106.028831f,28.623432f),
			new PointF (-106.028908f,28.623348f),
			new PointF (-106.029045f,28.623213f),
			new PointF (-106.028992f,28.622950f),
			new PointF (-106.028465f,28.621252f),
			new PointF (-106.028450f,28.621113f),
			new PointF (-106.028481f,28.621046f),
			new PointF (-106.028587f,28.620930f),
			new PointF (-106.028900f,28.620777f),
			new PointF (-106.029221f,28.620680f),
			new PointF (-106.040817f,28.622561f),
			new PointF (-106.042450f,28.622786f),
			new PointF (-106.042694f,28.622812f),
			new PointF (-106.042946f,28.622808f),
			new PointF (-106.043434f,28.622765f),
			new PointF (-106.044174f,28.622648f),
			new PointF (-106.046310f,28.622229f),
			new PointF (-106.046646f,28.622198f),
			new PointF (-106.046982f,28.622198f),
			new PointF (-106.047928f,28.622198f),
			new PointF (-106.048325f,28.622211f),
			new PointF (-106.048782f,28.622208f),
			new PointF (-106.049591f,28.622198f),
			new PointF (-106.049934f,28.622154f),
			new PointF (-106.050163f,28.622105f),
			new PointF (-106.050415f,28.622025f),
			new PointF (-106.050896f,28.621841f),
			new PointF (-106.052895f,28.621078f),
			new PointF (-106.056961f,28.619381f),
			new PointF (-106.057404f,28.619196f),
			new PointF (-106.057625f,28.619131f),
			new PointF (-106.057846f,28.619116f),
			new PointF (-106.058151f,28.619143f),
			new PointF (-106.058487f,28.619249f),
			new PointF (-106.059258f,28.619659f),
			new PointF (-106.062408f,28.621437f),
			new PointF (-106.072998f,28.627411f),
			new PointF (-106.073563f,28.627781f),
			new PointF (-106.073822f,28.628059f),
			new PointF (-106.074272f,28.628870f),
			new PointF (-106.072647f,28.633480f),
			new PointF (-106.073837f,28.634470f),
			new PointF (-106.076523f,28.636560f),
			new PointF (-106.078506f,28.638399f),
			new PointF (-106.078506f,28.638531f),
			new PointF (-106.075401f,28.641430f),
			new PointF (-106.075371f,28.641541f),
			new PointF (-106.076363f,28.642500f),
			new PointF (-106.084557f,28.650040f),
			new PointF (-106.085548f,28.651020f),
			new PointF (-106.085541f,28.651230f),
			new PointF (-106.085640f,28.651320f),
			new PointF (-106.085777f,28.651348f),
			new PointF (-106.085892f,28.651320f),
			new PointF (-106.086151f,28.651529f),
			new PointF (-106.095558f,28.660250f),
			new PointF (-106.096619f,28.661320f),
			new PointF (-106.097038f,28.661980f),
			new PointF (-106.098053f,28.664049f),
			new PointF (-106.103973f,28.676701f),
			new PointF (-106.104729f,28.678480f),
			new PointF (-106.110580f,28.683510f),
			new PointF (-106.110931f,28.683941f),
			new PointF (-106.111130f,28.684490f),
			new PointF (-106.111122f,28.685270f),
			new PointF (-106.110390f,28.687010f),
			new PointF (-106.110329f,28.687401f),
			new PointF (-106.110390f,28.687790f),
			new PointF (-106.110550f,28.688101f),
			new PointF (-106.123871f,28.701990f),
			new PointF (-106.124229f,28.702539f),
			new PointF (-106.125191f,28.703609f),
			new PointF (-106.125221f,28.703831f),
			new PointF (-106.125137f,28.704290f),
			new PointF (-106.124969f,28.704639f),
			new PointF (-106.124718f,28.704910f),
			new PointF (-106.124023f,28.705170f),
			new PointF (-106.123550f,28.704010f),
			new PointF (-106.124733f,28.703550f)
		};

		public static Station NotFoundStation()
		{
			return new Station{Latitude = Station.TroncalRouteCenter.Y, Longitude = Station.TroncalRouteCenter.X,
				Name = "Imposible obtener estación cercana"};
		}

		public string ImageFilename()
		{
			var originalFilename = Name.Replace("Estación","").TrimStart().Replace (" ", "_");
			var normalizedFilename = originalFilename.Normalize (System.Text.NormalizationForm.FormD);
			var reg = new Regex("[^a-zA-Z0-9_ ]");
			var imageFilename = reg.Replace (normalizedFilename, "");
			return  imageFilename;
		}
    }
}
