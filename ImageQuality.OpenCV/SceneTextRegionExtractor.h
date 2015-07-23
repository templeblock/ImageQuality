#pragma once

#include "Region.h"

using namespace System;
using namespace System::Collections::Generic;
using namespace System::IO;
using namespace std;
using namespace cv;

namespace ImageQuality {

	public ref class SceneTextRegionExtractor
	{
	private:
		const int MinWidth = 20;
		const int MinHeight = 12;

	public:
		IList<Region^>^ GetRegions(array<byte>^ buffer);
		IList<Region^>^ SimpleWatermark(array<byte>^ buffer);

	private:
		bool VerticalHeuristics(Mat roi);
		bool HorizontalHeuristics(Mat roi);
		vector<vector<double>> HeuristicSplit(vector<double> values);
		bool NeedsInverse(Mat roi);
		Nullable<float> FindBestAngle(vector<float> angles);
		IList<Region^>^ DetectAndRotate(Mat img, Scalar lower, Scalar upper);
		array<byte>^ ToByteArray(Mat image, const string& extension);
		void WriteToStream(const std::string& extension, Mat image, Stream^ stream);
		Mat ReadImage(array<byte>^ buffer);
	};
}