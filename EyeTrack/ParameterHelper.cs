﻿using VRC.SDK3.Avatars.ScriptableObjects;

namespace EyeTrack
{
    public static class ParameterHelper
    {
        public static int GetParamIndex(string paramName)
        {
            var parameters = new VRCExpressionParameters.Parameter[0];

            if (VRCPlayer
                .field_Internal_Static_VRCPlayer_0?.prop_VRCAvatarManager_0?.prop_VRCAvatarDescriptor_0
                ?.expressionParameters?.parameters != null)
                parameters = VRCPlayer
                    .field_Internal_Static_VRCPlayer_0
                    .prop_VRCAvatarManager_0.prop_VRCAvatarDescriptor_0.expressionParameters
                    .parameters;
            else
                return -1;

            var index = -1;
            for (var i = 0; i < parameters.Length; i++)
            {
                var param = parameters[i];
                if (param.name == null)
                    return -1;
                if (param.name == paramName) index = i;
            }

            return index;
        }

        public static void SetParameter(AvatarAnimParamController controller,
            MainMod.AV3Parameter param, float value)
        {
            if (controller == null || controller.field_Private_AvatarPlayableController_0 == null ||
                param.ParamIndex == -1)
                return;

            /*var paramsList = controller.field_Private_AvatarPlayableController_0
                .field_Private_ArrayOf_ObjectNPublicInObInPaInUnique_0;

            var paramSingle = paramsList[0].field_Public_ObjectPublicAnStInObLi1BoInSiBoUnique_0;
            var paramName = paramSingle.prop_String_0;
            var paramCurrentValue = paramSingle.field_Private_Single_0;*/
            //paramSingle.field_Private_Boolean_0;

			var param = GetParam(controller, param.ParamIndex);
			controller.field_Private_AvatarPlayableController_0.Method_Public_Void_Int32_Single_Boolean_0(param.prop_Int32_0, value);
            MelonLoader.MelonCoroutines.Start(UpdateParameters(controller));
        }
		
		public static ObjectPublicAnStInObLi1BoInSiBoUnique GetParam(AvatarAnimParamController controller, int index)
        {
            if (controller == null || controller.field_Private_AvatarPlayableController_0 == null || index == -1)
                return null;

            return controller.field_Private_AvatarPlayableController_0
                .field_Private_ArrayOf_ObjectNPublicInObInPaInUnique_0[index]
                .field_Public_ObjectPublicAnStInObLi1BoInSiBoUnique_0;
        }
		
		public static IEnumerator UpdateParameters(AvatarAnimParamController controller)
        {
            yield return new WaitForEndOfFrame(); // Needs a frame to update internally
            controller.field_Private_AvatarPlayableController_0.field_Private_Boolean_3 = true; // marks the parameters to be resynced over the network
        }
    }
}