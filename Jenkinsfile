node {
	stage 'Checkout'
		checkout scm

	stage 'Build'
		bat 'nuget restore LockScreenPictureGrabber.sln'
		bat "\"${tool 'MSBuild'}\" LockScreenPictureGrabber.sln /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"

	stage 'Archive'
		archive 'LockScreenPictureGrabber/bin/Release/**'

}
