using UnityEngine;

namespace PJL.Utilities.Extensions
{
    public static class SwizzleExtensions
    {
        public static Vector2 xx(this Quaternion q) => new(q.x, q.x);

        public static Vector2 xy(this Quaternion q) => new(q.x, q.y);

        public static Vector2 xz(this Quaternion q) => new(q.x, q.z);

        public static Vector2 xw(this Quaternion q) => new(q.x, q.w);

        public static Vector2 yx(this Quaternion q) => new(q.y, q.x);

        public static Vector2 yy(this Quaternion q) => new(q.y, q.y);

        public static Vector2 yz(this Quaternion q) => new(q.y, q.z);

        public static Vector2 yw(this Quaternion q) => new(q.y, q.w);

        public static Vector2 zx(this Quaternion q) => new(q.z, q.x);

        public static Vector2 zy(this Quaternion q) => new(q.z, q.y);

        public static Vector2 zz(this Quaternion q) => new(q.z, q.z);

        public static Vector2 zw(this Quaternion q) => new(q.z, q.w);

        public static Vector2 wx(this Quaternion q) => new(q.w, q.x);

        public static Vector2 wy(this Quaternion q) => new(q.w, q.y);

        public static Vector2 wz(this Quaternion q) => new(q.w, q.z);

        public static Vector2 ww(this Quaternion q) => new(q.w, q.w);

        public static Vector3 xxx(this Quaternion q) => new(q.x, q.x, q.x);

        public static Vector3 xxy(this Quaternion q) => new(q.x, q.x, q.y);

        public static Vector3 xxz(this Quaternion q) => new(q.x, q.x, q.z);

        public static Vector3 xxw(this Quaternion q) => new(q.x, q.x, q.w);

        public static Vector3 xyx(this Quaternion q) => new(q.x, q.y, q.x);

        public static Vector3 xyy(this Quaternion q) => new(q.x, q.y, q.y);

        public static Vector3 xyz(this Quaternion q) => new(q.x, q.y, q.z);

        public static Vector3 xyw(this Quaternion q) => new(q.x, q.y, q.w);

        public static Vector3 xzx(this Quaternion q) => new(q.x, q.z, q.x);

        public static Vector3 xzy(this Quaternion q) => new(q.x, q.z, q.y);

        public static Vector3 xzz(this Quaternion q) => new(q.x, q.z, q.z);

        public static Vector3 xzw(this Quaternion q) => new(q.x, q.z, q.w);

        public static Vector3 xwx(this Quaternion q) => new(q.x, q.w, q.x);

        public static Vector3 xwy(this Quaternion q) => new(q.x, q.w, q.y);

        public static Vector3 xwz(this Quaternion q) => new(q.x, q.w, q.z);

        public static Vector3 xww(this Quaternion q) => new(q.x, q.w, q.w);

        public static Vector3 yxx(this Quaternion q) => new(q.y, q.x, q.x);

        public static Vector3 yxy(this Quaternion q) => new(q.y, q.x, q.y);

        public static Vector3 yxz(this Quaternion q) => new(q.y, q.x, q.z);

        public static Vector3 yxw(this Quaternion q) => new(q.y, q.x, q.w);

        public static Vector3 yyx(this Quaternion q) => new(q.y, q.y, q.x);

        public static Vector3 yyy(this Quaternion q) => new(q.y, q.y, q.y);

        public static Vector3 yyz(this Quaternion q) => new(q.y, q.y, q.z);

        public static Vector3 yyw(this Quaternion q) => new(q.y, q.y, q.w);

        public static Vector3 yzx(this Quaternion q) => new(q.y, q.z, q.x);

        public static Vector3 yzy(this Quaternion q) => new(q.y, q.z, q.y);

        public static Vector3 yzz(this Quaternion q) => new(q.y, q.z, q.z);

        public static Vector3 yzw(this Quaternion q) => new(q.y, q.z, q.w);

        public static Vector3 ywx(this Quaternion q) => new(q.y, q.w, q.x);

        public static Vector3 ywy(this Quaternion q) => new(q.y, q.w, q.y);

        public static Vector3 ywz(this Quaternion q) => new(q.y, q.w, q.z);

        public static Vector3 yww(this Quaternion q) => new(q.y, q.w, q.w);

        public static Vector3 zxx(this Quaternion q) => new(q.z, q.x, q.x);

        public static Vector3 zxy(this Quaternion q) => new(q.z, q.x, q.y);

        public static Vector3 zxz(this Quaternion q) => new(q.z, q.x, q.z);

        public static Vector3 zxw(this Quaternion q) => new(q.z, q.x, q.w);

        public static Vector3 zyx(this Quaternion q) => new(q.z, q.y, q.x);

        public static Vector3 zyy(this Quaternion q) => new(q.z, q.y, q.y);

        public static Vector3 zyz(this Quaternion q) => new(q.z, q.y, q.z);

        public static Vector3 zyw(this Quaternion q) => new(q.z, q.y, q.w);

        public static Vector3 zzx(this Quaternion q) => new(q.z, q.z, q.x);

        public static Vector3 zzy(this Quaternion q) => new(q.z, q.z, q.y);

        public static Vector3 zzz(this Quaternion q) => new(q.z, q.z, q.z);

        public static Vector3 zzw(this Quaternion q) => new(q.z, q.z, q.w);

        public static Vector3 zwx(this Quaternion q) => new(q.z, q.w, q.x);

        public static Vector3 zwy(this Quaternion q) => new(q.z, q.w, q.y);

        public static Vector3 zwz(this Quaternion q) => new(q.z, q.w, q.z);

        public static Vector3 zww(this Quaternion q) => new(q.z, q.w, q.w);

        public static Vector3 wxx(this Quaternion q) => new(q.w, q.x, q.x);

        public static Vector3 wxy(this Quaternion q) => new(q.w, q.x, q.y);

        public static Vector3 wxz(this Quaternion q) => new(q.w, q.x, q.z);

        public static Vector3 wxw(this Quaternion q) => new(q.w, q.x, q.w);

        public static Vector3 wyx(this Quaternion q) => new(q.w, q.y, q.x);

        public static Vector3 wyy(this Quaternion q) => new(q.w, q.y, q.y);

        public static Vector3 wyz(this Quaternion q) => new(q.w, q.y, q.z);

        public static Vector3 wyw(this Quaternion q) => new(q.w, q.y, q.w);

        public static Vector3 wzx(this Quaternion q) => new(q.w, q.z, q.x);

        public static Vector3 wzy(this Quaternion q) => new(q.w, q.z, q.y);

        public static Vector3 wzz(this Quaternion q) => new(q.w, q.z, q.z);

        public static Vector3 wzw(this Quaternion q) => new(q.w, q.z, q.w);

        public static Vector3 wwx(this Quaternion q) => new(q.w, q.w, q.x);

        public static Vector3 wwy(this Quaternion q) => new(q.w, q.w, q.y);

        public static Vector3 wwz(this Quaternion q) => new(q.w, q.w, q.z);

        public static Vector3 www(this Quaternion q) => new(q.w, q.w, q.w);

        public static Vector4 xxxx(this Quaternion q) => new(q.x, q.x, q.x, q.x);

        public static Vector4 xxxy(this Quaternion q) => new(q.x, q.x, q.x, q.y);

        public static Vector4 xxxz(this Quaternion q) => new(q.x, q.x, q.x, q.z);

        public static Vector4 xxxw(this Quaternion q) => new(q.x, q.x, q.x, q.w);

        public static Vector4 xxyx(this Quaternion q) => new(q.x, q.x, q.y, q.x);

        public static Vector4 xxyy(this Quaternion q) => new(q.x, q.x, q.y, q.y);

        public static Vector4 xxyz(this Quaternion q) => new(q.x, q.x, q.y, q.z);

        public static Vector4 xxyw(this Quaternion q) => new(q.x, q.x, q.y, q.w);

        public static Vector4 xxzx(this Quaternion q) => new(q.x, q.x, q.z, q.x);

        public static Vector4 xxzy(this Quaternion q) => new(q.x, q.x, q.z, q.y);

        public static Vector4 xxzz(this Quaternion q) => new(q.x, q.x, q.z, q.z);

        public static Vector4 xxzw(this Quaternion q) => new(q.x, q.x, q.z, q.w);

        public static Vector4 xxwx(this Quaternion q) => new(q.x, q.x, q.w, q.x);

        public static Vector4 xxwy(this Quaternion q) => new(q.x, q.x, q.w, q.y);

        public static Vector4 xxwz(this Quaternion q) => new(q.x, q.x, q.w, q.z);

        public static Vector4 xxww(this Quaternion q) => new(q.x, q.x, q.w, q.w);

        public static Vector4 xyxx(this Quaternion q) => new(q.x, q.y, q.x, q.x);

        public static Vector4 xyxy(this Quaternion q) => new(q.x, q.y, q.x, q.y);

        public static Vector4 xyxz(this Quaternion q) => new(q.x, q.y, q.x, q.z);

        public static Vector4 xyxw(this Quaternion q) => new(q.x, q.y, q.x, q.w);

        public static Vector4 xyyx(this Quaternion q) => new(q.x, q.y, q.y, q.x);

        public static Vector4 xyyy(this Quaternion q) => new(q.x, q.y, q.y, q.y);

        public static Vector4 xyyz(this Quaternion q) => new(q.x, q.y, q.y, q.z);

        public static Vector4 xyyw(this Quaternion q) => new(q.x, q.y, q.y, q.w);

        public static Vector4 xyzx(this Quaternion q) => new(q.x, q.y, q.z, q.x);

        public static Vector4 xyzy(this Quaternion q) => new(q.x, q.y, q.z, q.y);

        public static Vector4 xyzz(this Quaternion q) => new(q.x, q.y, q.z, q.z);

        public static Vector4 xyzw(this Quaternion q) => new(q.x, q.y, q.z, q.w);

        public static Vector4 xywx(this Quaternion q) => new(q.x, q.y, q.w, q.x);

        public static Vector4 xywy(this Quaternion q) => new(q.x, q.y, q.w, q.y);

        public static Vector4 xywz(this Quaternion q) => new(q.x, q.y, q.w, q.z);

        public static Vector4 xyww(this Quaternion q) => new(q.x, q.y, q.w, q.w);

        public static Vector4 xzxx(this Quaternion q) => new(q.x, q.z, q.x, q.x);

        public static Vector4 xzxy(this Quaternion q) => new(q.x, q.z, q.x, q.y);

        public static Vector4 xzxz(this Quaternion q) => new(q.x, q.z, q.x, q.z);

        public static Vector4 xzxw(this Quaternion q) => new(q.x, q.z, q.x, q.w);

        public static Vector4 xzyx(this Quaternion q) => new(q.x, q.z, q.y, q.x);

        public static Vector4 xzyy(this Quaternion q) => new(q.x, q.z, q.y, q.y);

        public static Vector4 xzyz(this Quaternion q) => new(q.x, q.z, q.y, q.z);

        public static Vector4 xzyw(this Quaternion q) => new(q.x, q.z, q.y, q.w);

        public static Vector4 xzzx(this Quaternion q) => new(q.x, q.z, q.z, q.x);

        public static Vector4 xzzy(this Quaternion q) => new(q.x, q.z, q.z, q.y);

        public static Vector4 xzzz(this Quaternion q) => new(q.x, q.z, q.z, q.z);

        public static Vector4 xzzw(this Quaternion q) => new(q.x, q.z, q.z, q.w);

        public static Vector4 xzwx(this Quaternion q) => new(q.x, q.z, q.w, q.x);

        public static Vector4 xzwy(this Quaternion q) => new(q.x, q.z, q.w, q.y);

        public static Vector4 xzwz(this Quaternion q) => new(q.x, q.z, q.w, q.z);

        public static Vector4 xzww(this Quaternion q) => new(q.x, q.z, q.w, q.w);

        public static Vector4 xwxx(this Quaternion q) => new(q.x, q.w, q.x, q.x);

        public static Vector4 xwxy(this Quaternion q) => new(q.x, q.w, q.x, q.y);

        public static Vector4 xwxz(this Quaternion q) => new(q.x, q.w, q.x, q.z);

        public static Vector4 xwxw(this Quaternion q) => new(q.x, q.w, q.x, q.w);

        public static Vector4 xwyx(this Quaternion q) => new(q.x, q.w, q.y, q.x);

        public static Vector4 xwyy(this Quaternion q) => new(q.x, q.w, q.y, q.y);

        public static Vector4 xwyz(this Quaternion q) => new(q.x, q.w, q.y, q.z);

        public static Vector4 xwyw(this Quaternion q) => new(q.x, q.w, q.y, q.w);

        public static Vector4 xwzx(this Quaternion q) => new(q.x, q.w, q.z, q.x);

        public static Vector4 xwzy(this Quaternion q) => new(q.x, q.w, q.z, q.y);

        public static Vector4 xwzz(this Quaternion q) => new(q.x, q.w, q.z, q.z);

        public static Vector4 xwzw(this Quaternion q) => new(q.x, q.w, q.z, q.w);

        public static Vector4 xwwx(this Quaternion q) => new(q.x, q.w, q.w, q.x);

        public static Vector4 xwwy(this Quaternion q) => new(q.x, q.w, q.w, q.y);

        public static Vector4 xwwz(this Quaternion q) => new(q.x, q.w, q.w, q.z);

        public static Vector4 xwww(this Quaternion q) => new(q.x, q.w, q.w, q.w);

        public static Vector4 yxxx(this Quaternion q) => new(q.y, q.x, q.x, q.x);

        public static Vector4 yxxy(this Quaternion q) => new(q.y, q.x, q.x, q.y);

        public static Vector4 yxxz(this Quaternion q) => new(q.y, q.x, q.x, q.z);

        public static Vector4 yxxw(this Quaternion q) => new(q.y, q.x, q.x, q.w);

        public static Vector4 yxyx(this Quaternion q) => new(q.y, q.x, q.y, q.x);

        public static Vector4 yxyy(this Quaternion q) => new(q.y, q.x, q.y, q.y);

        public static Vector4 yxyz(this Quaternion q) => new(q.y, q.x, q.y, q.z);

        public static Vector4 yxyw(this Quaternion q) => new(q.y, q.x, q.y, q.w);

        public static Vector4 yxzx(this Quaternion q) => new(q.y, q.x, q.z, q.x);

        public static Vector4 yxzy(this Quaternion q) => new(q.y, q.x, q.z, q.y);

        public static Vector4 yxzz(this Quaternion q) => new(q.y, q.x, q.z, q.z);

        public static Vector4 yxzw(this Quaternion q) => new(q.y, q.x, q.z, q.w);

        public static Vector4 yxwx(this Quaternion q) => new(q.y, q.x, q.w, q.x);

        public static Vector4 yxwy(this Quaternion q) => new(q.y, q.x, q.w, q.y);

        public static Vector4 yxwz(this Quaternion q) => new(q.y, q.x, q.w, q.z);

        public static Vector4 yxww(this Quaternion q) => new(q.y, q.x, q.w, q.w);

        public static Vector4 yyxx(this Quaternion q) => new(q.y, q.y, q.x, q.x);

        public static Vector4 yyxy(this Quaternion q) => new(q.y, q.y, q.x, q.y);

        public static Vector4 yyxz(this Quaternion q) => new(q.y, q.y, q.x, q.z);

        public static Vector4 yyxw(this Quaternion q) => new(q.y, q.y, q.x, q.w);

        public static Vector4 yyyx(this Quaternion q) => new(q.y, q.y, q.y, q.x);

        public static Vector4 yyyy(this Quaternion q) => new(q.y, q.y, q.y, q.y);

        public static Vector4 yyyz(this Quaternion q) => new(q.y, q.y, q.y, q.z);

        public static Vector4 yyyw(this Quaternion q) => new(q.y, q.y, q.y, q.w);

        public static Vector4 yyzx(this Quaternion q) => new(q.y, q.y, q.z, q.x);

        public static Vector4 yyzy(this Quaternion q) => new(q.y, q.y, q.z, q.y);

        public static Vector4 yyzz(this Quaternion q) => new(q.y, q.y, q.z, q.z);

        public static Vector4 yyzw(this Quaternion q) => new(q.y, q.y, q.z, q.w);

        public static Vector4 yywx(this Quaternion q) => new(q.y, q.y, q.w, q.x);

        public static Vector4 yywy(this Quaternion q) => new(q.y, q.y, q.w, q.y);

        public static Vector4 yywz(this Quaternion q) => new(q.y, q.y, q.w, q.z);

        public static Vector4 yyww(this Quaternion q) => new(q.y, q.y, q.w, q.w);

        public static Vector4 yzxx(this Quaternion q) => new(q.y, q.z, q.x, q.x);

        public static Vector4 yzxy(this Quaternion q) => new(q.y, q.z, q.x, q.y);

        public static Vector4 yzxz(this Quaternion q) => new(q.y, q.z, q.x, q.z);

        public static Vector4 yzxw(this Quaternion q) => new(q.y, q.z, q.x, q.w);

        public static Vector4 yzyx(this Quaternion q) => new(q.y, q.z, q.y, q.x);

        public static Vector4 yzyy(this Quaternion q) => new(q.y, q.z, q.y, q.y);

        public static Vector4 yzyz(this Quaternion q) => new(q.y, q.z, q.y, q.z);

        public static Vector4 yzyw(this Quaternion q) => new(q.y, q.z, q.y, q.w);

        public static Vector4 yzzx(this Quaternion q) => new(q.y, q.z, q.z, q.x);

        public static Vector4 yzzy(this Quaternion q) => new(q.y, q.z, q.z, q.y);

        public static Vector4 yzzz(this Quaternion q) => new(q.y, q.z, q.z, q.z);

        public static Vector4 yzzw(this Quaternion q) => new(q.y, q.z, q.z, q.w);

        public static Vector4 yzwx(this Quaternion q) => new(q.y, q.z, q.w, q.x);

        public static Vector4 yzwy(this Quaternion q) => new(q.y, q.z, q.w, q.y);

        public static Vector4 yzwz(this Quaternion q) => new(q.y, q.z, q.w, q.z);

        public static Vector4 yzww(this Quaternion q) => new(q.y, q.z, q.w, q.w);

        public static Vector4 ywxx(this Quaternion q) => new(q.y, q.w, q.x, q.x);

        public static Vector4 ywxy(this Quaternion q) => new(q.y, q.w, q.x, q.y);

        public static Vector4 ywxz(this Quaternion q) => new(q.y, q.w, q.x, q.z);

        public static Vector4 ywxw(this Quaternion q) => new(q.y, q.w, q.x, q.w);

        public static Vector4 ywyx(this Quaternion q) => new(q.y, q.w, q.y, q.x);

        public static Vector4 ywyy(this Quaternion q) => new(q.y, q.w, q.y, q.y);

        public static Vector4 ywyz(this Quaternion q) => new(q.y, q.w, q.y, q.z);

        public static Vector4 ywyw(this Quaternion q) => new(q.y, q.w, q.y, q.w);

        public static Vector4 ywzx(this Quaternion q) => new(q.y, q.w, q.z, q.x);

        public static Vector4 ywzy(this Quaternion q) => new(q.y, q.w, q.z, q.y);

        public static Vector4 ywzz(this Quaternion q) => new(q.y, q.w, q.z, q.z);

        public static Vector4 ywzw(this Quaternion q) => new(q.y, q.w, q.z, q.w);

        public static Vector4 ywwx(this Quaternion q) => new(q.y, q.w, q.w, q.x);

        public static Vector4 ywwy(this Quaternion q) => new(q.y, q.w, q.w, q.y);

        public static Vector4 ywwz(this Quaternion q) => new(q.y, q.w, q.w, q.z);

        public static Vector4 ywww(this Quaternion q) => new(q.y, q.w, q.w, q.w);

        public static Vector4 zxxx(this Quaternion q) => new(q.z, q.x, q.x, q.x);

        public static Vector4 zxxy(this Quaternion q) => new(q.z, q.x, q.x, q.y);

        public static Vector4 zxxz(this Quaternion q) => new(q.z, q.x, q.x, q.z);

        public static Vector4 zxxw(this Quaternion q) => new(q.z, q.x, q.x, q.w);

        public static Vector4 zxyx(this Quaternion q) => new(q.z, q.x, q.y, q.x);

        public static Vector4 zxyy(this Quaternion q) => new(q.z, q.x, q.y, q.y);

        public static Vector4 zxyz(this Quaternion q) => new(q.z, q.x, q.y, q.z);

        public static Vector4 zxyw(this Quaternion q) => new(q.z, q.x, q.y, q.w);

        public static Vector4 zxzx(this Quaternion q) => new(q.z, q.x, q.z, q.x);

        public static Vector4 zxzy(this Quaternion q) => new(q.z, q.x, q.z, q.y);

        public static Vector4 zxzz(this Quaternion q) => new(q.z, q.x, q.z, q.z);

        public static Vector4 zxzw(this Quaternion q) => new(q.z, q.x, q.z, q.w);

        public static Vector4 zxwx(this Quaternion q) => new(q.z, q.x, q.w, q.x);

        public static Vector4 zxwy(this Quaternion q) => new(q.z, q.x, q.w, q.y);

        public static Vector4 zxwz(this Quaternion q) => new(q.z, q.x, q.w, q.z);

        public static Vector4 zxww(this Quaternion q) => new(q.z, q.x, q.w, q.w);

        public static Vector4 zyxx(this Quaternion q) => new(q.z, q.y, q.x, q.x);

        public static Vector4 zyxy(this Quaternion q) => new(q.z, q.y, q.x, q.y);

        public static Vector4 zyxz(this Quaternion q) => new(q.z, q.y, q.x, q.z);

        public static Vector4 zyxw(this Quaternion q) => new(q.z, q.y, q.x, q.w);

        public static Vector4 zyyx(this Quaternion q) => new(q.z, q.y, q.y, q.x);

        public static Vector4 zyyy(this Quaternion q) => new(q.z, q.y, q.y, q.y);

        public static Vector4 zyyz(this Quaternion q) => new(q.z, q.y, q.y, q.z);

        public static Vector4 zyyw(this Quaternion q) => new(q.z, q.y, q.y, q.w);

        public static Vector4 zyzx(this Quaternion q) => new(q.z, q.y, q.z, q.x);

        public static Vector4 zyzy(this Quaternion q) => new(q.z, q.y, q.z, q.y);

        public static Vector4 zyzz(this Quaternion q) => new(q.z, q.y, q.z, q.z);

        public static Vector4 zyzw(this Quaternion q) => new(q.z, q.y, q.z, q.w);

        public static Vector4 zywx(this Quaternion q) => new(q.z, q.y, q.w, q.x);

        public static Vector4 zywy(this Quaternion q) => new(q.z, q.y, q.w, q.y);

        public static Vector4 zywz(this Quaternion q) => new(q.z, q.y, q.w, q.z);

        public static Vector4 zyww(this Quaternion q) => new(q.z, q.y, q.w, q.w);

        public static Vector4 zzxx(this Quaternion q) => new(q.z, q.z, q.x, q.x);

        public static Vector4 zzxy(this Quaternion q) => new(q.z, q.z, q.x, q.y);

        public static Vector4 zzxz(this Quaternion q) => new(q.z, q.z, q.x, q.z);

        public static Vector4 zzxw(this Quaternion q) => new(q.z, q.z, q.x, q.w);

        public static Vector4 zzyx(this Quaternion q) => new(q.z, q.z, q.y, q.x);

        public static Vector4 zzyy(this Quaternion q) => new(q.z, q.z, q.y, q.y);

        public static Vector4 zzyz(this Quaternion q) => new(q.z, q.z, q.y, q.z);

        public static Vector4 zzyw(this Quaternion q) => new(q.z, q.z, q.y, q.w);

        public static Vector4 zzzx(this Quaternion q) => new(q.z, q.z, q.z, q.x);

        public static Vector4 zzzy(this Quaternion q) => new(q.z, q.z, q.z, q.y);

        public static Vector4 zzzz(this Quaternion q) => new(q.z, q.z, q.z, q.z);

        public static Vector4 zzzw(this Quaternion q) => new(q.z, q.z, q.z, q.w);

        public static Vector4 zzwx(this Quaternion q) => new(q.z, q.z, q.w, q.x);

        public static Vector4 zzwy(this Quaternion q) => new(q.z, q.z, q.w, q.y);

        public static Vector4 zzwz(this Quaternion q) => new(q.z, q.z, q.w, q.z);

        public static Vector4 zzww(this Quaternion q) => new(q.z, q.z, q.w, q.w);

        public static Vector4 zwxx(this Quaternion q) => new(q.z, q.w, q.x, q.x);

        public static Vector4 zwxy(this Quaternion q) => new(q.z, q.w, q.x, q.y);

        public static Vector4 zwxz(this Quaternion q) => new(q.z, q.w, q.x, q.z);

        public static Vector4 zwxw(this Quaternion q) => new(q.z, q.w, q.x, q.w);

        public static Vector4 zwyx(this Quaternion q) => new(q.z, q.w, q.y, q.x);

        public static Vector4 zwyy(this Quaternion q) => new(q.z, q.w, q.y, q.y);

        public static Vector4 zwyz(this Quaternion q) => new(q.z, q.w, q.y, q.z);

        public static Vector4 zwyw(this Quaternion q) => new(q.z, q.w, q.y, q.w);

        public static Vector4 zwzx(this Quaternion q) => new(q.z, q.w, q.z, q.x);

        public static Vector4 zwzy(this Quaternion q) => new(q.z, q.w, q.z, q.y);

        public static Vector4 zwzz(this Quaternion q) => new(q.z, q.w, q.z, q.z);

        public static Vector4 zwzw(this Quaternion q) => new(q.z, q.w, q.z, q.w);

        public static Vector4 zwwx(this Quaternion q) => new(q.z, q.w, q.w, q.x);

        public static Vector4 zwwy(this Quaternion q) => new(q.z, q.w, q.w, q.y);

        public static Vector4 zwwz(this Quaternion q) => new(q.z, q.w, q.w, q.z);

        public static Vector4 zwww(this Quaternion q) => new(q.z, q.w, q.w, q.w);

        public static Vector4 wxxx(this Quaternion q) => new(q.w, q.x, q.x, q.x);

        public static Vector4 wxxy(this Quaternion q) => new(q.w, q.x, q.x, q.y);

        public static Vector4 wxxz(this Quaternion q) => new(q.w, q.x, q.x, q.z);

        public static Vector4 wxxw(this Quaternion q) => new(q.w, q.x, q.x, q.w);

        public static Vector4 wxyx(this Quaternion q) => new(q.w, q.x, q.y, q.x);

        public static Vector4 wxyy(this Quaternion q) => new(q.w, q.x, q.y, q.y);

        public static Vector4 wxyz(this Quaternion q) => new(q.w, q.x, q.y, q.z);

        public static Vector4 wxyw(this Quaternion q) => new(q.w, q.x, q.y, q.w);

        public static Vector4 wxzx(this Quaternion q) => new(q.w, q.x, q.z, q.x);

        public static Vector4 wxzy(this Quaternion q) => new(q.w, q.x, q.z, q.y);

        public static Vector4 wxzz(this Quaternion q) => new(q.w, q.x, q.z, q.z);

        public static Vector4 wxzw(this Quaternion q) => new(q.w, q.x, q.z, q.w);

        public static Vector4 wxwx(this Quaternion q) => new(q.w, q.x, q.w, q.x);

        public static Vector4 wxwy(this Quaternion q) => new(q.w, q.x, q.w, q.y);

        public static Vector4 wxwz(this Quaternion q) => new(q.w, q.x, q.w, q.z);

        public static Vector4 wxww(this Quaternion q) => new(q.w, q.x, q.w, q.w);

        public static Vector4 wyxx(this Quaternion q) => new(q.w, q.y, q.x, q.x);

        public static Vector4 wyxy(this Quaternion q) => new(q.w, q.y, q.x, q.y);

        public static Vector4 wyxz(this Quaternion q) => new(q.w, q.y, q.x, q.z);

        public static Vector4 wyxw(this Quaternion q) => new(q.w, q.y, q.x, q.w);

        public static Vector4 wyyx(this Quaternion q) => new(q.w, q.y, q.y, q.x);

        public static Vector4 wyyy(this Quaternion q) => new(q.w, q.y, q.y, q.y);

        public static Vector4 wyyz(this Quaternion q) => new(q.w, q.y, q.y, q.z);

        public static Vector4 wyyw(this Quaternion q) => new(q.w, q.y, q.y, q.w);

        public static Vector4 wyzx(this Quaternion q) => new(q.w, q.y, q.z, q.x);

        public static Vector4 wyzy(this Quaternion q) => new(q.w, q.y, q.z, q.y);

        public static Vector4 wyzz(this Quaternion q) => new(q.w, q.y, q.z, q.z);

        public static Vector4 wyzw(this Quaternion q) => new(q.w, q.y, q.z, q.w);

        public static Vector4 wywx(this Quaternion q) => new(q.w, q.y, q.w, q.x);

        public static Vector4 wywy(this Quaternion q) => new(q.w, q.y, q.w, q.y);

        public static Vector4 wywz(this Quaternion q) => new(q.w, q.y, q.w, q.z);

        public static Vector4 wyww(this Quaternion q) => new(q.w, q.y, q.w, q.w);

        public static Vector4 wzxx(this Quaternion q) => new(q.w, q.z, q.x, q.x);

        public static Vector4 wzxy(this Quaternion q) => new(q.w, q.z, q.x, q.y);

        public static Vector4 wzxz(this Quaternion q) => new(q.w, q.z, q.x, q.z);

        public static Vector4 wzxw(this Quaternion q) => new(q.w, q.z, q.x, q.w);

        public static Vector4 wzyx(this Quaternion q) => new(q.w, q.z, q.y, q.x);

        public static Vector4 wzyy(this Quaternion q) => new(q.w, q.z, q.y, q.y);

        public static Vector4 wzyz(this Quaternion q) => new(q.w, q.z, q.y, q.z);

        public static Vector4 wzyw(this Quaternion q) => new(q.w, q.z, q.y, q.w);

        public static Vector4 wzzx(this Quaternion q) => new(q.w, q.z, q.z, q.x);

        public static Vector4 wzzy(this Quaternion q) => new(q.w, q.z, q.z, q.y);

        public static Vector4 wzzz(this Quaternion q) => new(q.w, q.z, q.z, q.z);

        public static Vector4 wzzw(this Quaternion q) => new(q.w, q.z, q.z, q.w);

        public static Vector4 wzwx(this Quaternion q) => new(q.w, q.z, q.w, q.x);

        public static Vector4 wzwy(this Quaternion q) => new(q.w, q.z, q.w, q.y);

        public static Vector4 wzwz(this Quaternion q) => new(q.w, q.z, q.w, q.z);

        public static Vector4 wzww(this Quaternion q) => new(q.w, q.z, q.w, q.w);

        public static Vector4 wwxx(this Quaternion q) => new(q.w, q.w, q.x, q.x);

        public static Vector4 wwxy(this Quaternion q) => new(q.w, q.w, q.x, q.y);

        public static Vector4 wwxz(this Quaternion q) => new(q.w, q.w, q.x, q.z);

        public static Vector4 wwxw(this Quaternion q) => new(q.w, q.w, q.x, q.w);

        public static Vector4 wwyx(this Quaternion q) => new(q.w, q.w, q.y, q.x);

        public static Vector4 wwyy(this Quaternion q) => new(q.w, q.w, q.y, q.y);

        public static Vector4 wwyz(this Quaternion q) => new(q.w, q.w, q.y, q.z);

        public static Vector4 wwyw(this Quaternion q) => new(q.w, q.w, q.y, q.w);

        public static Vector4 wwzx(this Quaternion q) => new(q.w, q.w, q.z, q.x);

        public static Vector4 wwzy(this Quaternion q) => new(q.w, q.w, q.z, q.y);

        public static Vector4 wwzz(this Quaternion q) => new(q.w, q.w, q.z, q.z);

        public static Vector4 wwzw(this Quaternion q) => new(q.w, q.w, q.z, q.w);

        public static Vector4 wwwx(this Quaternion q) => new(q.w, q.w, q.w, q.x);

        public static Vector4 wwwy(this Quaternion q) => new(q.w, q.w, q.w, q.y);

        public static Vector4 wwwz(this Quaternion q) => new(q.w, q.w, q.w, q.z);

        public static Vector4 wwww(this Quaternion q) => new(q.w, q.w, q.w, q.w);

        public static Vector2 xx(this Vector4 v) => new(v.x, v.x);

        public static Vector2 xy(this Vector4 v) => new(v.x, v.y);

        public static Vector2 xz(this Vector4 v) => new(v.x, v.z);

        public static Vector2 xw(this Vector4 v) => new(v.x, v.w);

        public static Vector2 yx(this Vector4 v) => new(v.y, v.x);

        public static Vector2 yy(this Vector4 v) => new(v.y, v.y);

        public static Vector2 yz(this Vector4 v) => new(v.y, v.z);

        public static Vector2 yw(this Vector4 v) => new(v.y, v.w);

        public static Vector2 zx(this Vector4 v) => new(v.z, v.x);

        public static Vector2 zy(this Vector4 v) => new(v.z, v.y);

        public static Vector2 zz(this Vector4 v) => new(v.z, v.z);

        public static Vector2 zw(this Vector4 v) => new(v.z, v.w);

        public static Vector2 wx(this Vector4 v) => new(v.w, v.x);

        public static Vector2 wy(this Vector4 v) => new(v.w, v.y);

        public static Vector2 wz(this Vector4 v) => new(v.w, v.z);

        public static Vector2 ww(this Vector4 v) => new(v.w, v.w);

        public static Vector3 xxx(this Vector4 v) => new(v.x, v.x, v.x);

        public static Vector3 xxy(this Vector4 v) => new(v.x, v.x, v.y);

        public static Vector3 xxz(this Vector4 v) => new(v.x, v.x, v.z);

        public static Vector3 xxw(this Vector4 v) => new(v.x, v.x, v.w);

        public static Vector3 xyx(this Vector4 v) => new(v.x, v.y, v.x);

        public static Vector3 xyy(this Vector4 v) => new(v.x, v.y, v.y);

        public static Vector3 xyz(this Vector4 v) => new(v.x, v.y, v.z);

        public static Vector3 xyw(this Vector4 v) => new(v.x, v.y, v.w);

        public static Vector3 xzx(this Vector4 v) => new(v.x, v.z, v.x);

        public static Vector3 xzy(this Vector4 v) => new(v.x, v.z, v.y);

        public static Vector3 xzz(this Vector4 v) => new(v.x, v.z, v.z);

        public static Vector3 xzw(this Vector4 v) => new(v.x, v.z, v.w);

        public static Vector3 xwx(this Vector4 v) => new(v.x, v.w, v.x);

        public static Vector3 xwy(this Vector4 v) => new(v.x, v.w, v.y);

        public static Vector3 xwz(this Vector4 v) => new(v.x, v.w, v.z);

        public static Vector3 xww(this Vector4 v) => new(v.x, v.w, v.w);

        public static Vector3 yxx(this Vector4 v) => new(v.y, v.x, v.x);

        public static Vector3 yxy(this Vector4 v) => new(v.y, v.x, v.y);

        public static Vector3 yxz(this Vector4 v) => new(v.y, v.x, v.z);

        public static Vector3 yxw(this Vector4 v) => new(v.y, v.x, v.w);

        public static Vector3 yyx(this Vector4 v) => new(v.y, v.y, v.x);

        public static Vector3 yyy(this Vector4 v) => new(v.y, v.y, v.y);

        public static Vector3 yyz(this Vector4 v) => new(v.y, v.y, v.z);

        public static Vector3 yyw(this Vector4 v) => new(v.y, v.y, v.w);

        public static Vector3 yzx(this Vector4 v) => new(v.y, v.z, v.x);

        public static Vector3 yzy(this Vector4 v) => new(v.y, v.z, v.y);

        public static Vector3 yzz(this Vector4 v) => new(v.y, v.z, v.z);

        public static Vector3 yzw(this Vector4 v) => new(v.y, v.z, v.w);

        public static Vector3 ywx(this Vector4 v) => new(v.y, v.w, v.x);

        public static Vector3 ywy(this Vector4 v) => new(v.y, v.w, v.y);

        public static Vector3 ywz(this Vector4 v) => new(v.y, v.w, v.z);

        public static Vector3 yww(this Vector4 v) => new(v.y, v.w, v.w);

        public static Vector3 zxx(this Vector4 v) => new(v.z, v.x, v.x);

        public static Vector3 zxy(this Vector4 v) => new(v.z, v.x, v.y);

        public static Vector3 zxz(this Vector4 v) => new(v.z, v.x, v.z);

        public static Vector3 zxw(this Vector4 v) => new(v.z, v.x, v.w);

        public static Vector3 zyx(this Vector4 v) => new(v.z, v.y, v.x);

        public static Vector3 zyy(this Vector4 v) => new(v.z, v.y, v.y);

        public static Vector3 zyz(this Vector4 v) => new(v.z, v.y, v.z);

        public static Vector3 zyw(this Vector4 v) => new(v.z, v.y, v.w);

        public static Vector3 zzx(this Vector4 v) => new(v.z, v.z, v.x);

        public static Vector3 zzy(this Vector4 v) => new(v.z, v.z, v.y);

        public static Vector3 zzz(this Vector4 v) => new(v.z, v.z, v.z);

        public static Vector3 zzw(this Vector4 v) => new(v.z, v.z, v.w);

        public static Vector3 zwx(this Vector4 v) => new(v.z, v.w, v.x);

        public static Vector3 zwy(this Vector4 v) => new(v.z, v.w, v.y);

        public static Vector3 zwz(this Vector4 v) => new(v.z, v.w, v.z);

        public static Vector3 zww(this Vector4 v) => new(v.z, v.w, v.w);

        public static Vector3 wxx(this Vector4 v) => new(v.w, v.x, v.x);

        public static Vector3 wxy(this Vector4 v) => new(v.w, v.x, v.y);

        public static Vector3 wxz(this Vector4 v) => new(v.w, v.x, v.z);

        public static Vector3 wxw(this Vector4 v) => new(v.w, v.x, v.w);

        public static Vector3 wyx(this Vector4 v) => new(v.w, v.y, v.x);

        public static Vector3 wyy(this Vector4 v) => new(v.w, v.y, v.y);

        public static Vector3 wyz(this Vector4 v) => new(v.w, v.y, v.z);

        public static Vector3 wyw(this Vector4 v) => new(v.w, v.y, v.w);

        public static Vector3 wzx(this Vector4 v) => new(v.w, v.z, v.x);

        public static Vector3 wzy(this Vector4 v) => new(v.w, v.z, v.y);

        public static Vector3 wzz(this Vector4 v) => new(v.w, v.z, v.z);

        public static Vector3 wzw(this Vector4 v) => new(v.w, v.z, v.w);

        public static Vector3 wwx(this Vector4 v) => new(v.w, v.w, v.x);

        public static Vector3 wwy(this Vector4 v) => new(v.w, v.w, v.y);

        public static Vector3 wwz(this Vector4 v) => new(v.w, v.w, v.z);

        public static Vector3 www(this Vector4 v) => new(v.w, v.w, v.w);

        public static Vector4 xxxx(this Vector4 v) => new(v.x, v.x, v.x, v.x);

        public static Vector4 xxxy(this Vector4 v) => new(v.x, v.x, v.x, v.y);

        public static Vector4 xxxz(this Vector4 v) => new(v.x, v.x, v.x, v.z);

        public static Vector4 xxxw(this Vector4 v) => new(v.x, v.x, v.x, v.w);

        public static Vector4 xxyx(this Vector4 v) => new(v.x, v.x, v.y, v.x);

        public static Vector4 xxyy(this Vector4 v) => new(v.x, v.x, v.y, v.y);

        public static Vector4 xxyz(this Vector4 v) => new(v.x, v.x, v.y, v.z);

        public static Vector4 xxyw(this Vector4 v) => new(v.x, v.x, v.y, v.w);

        public static Vector4 xxzx(this Vector4 v) => new(v.x, v.x, v.z, v.x);

        public static Vector4 xxzy(this Vector4 v) => new(v.x, v.x, v.z, v.y);

        public static Vector4 xxzz(this Vector4 v) => new(v.x, v.x, v.z, v.z);

        public static Vector4 xxzw(this Vector4 v) => new(v.x, v.x, v.z, v.w);

        public static Vector4 xxwx(this Vector4 v) => new(v.x, v.x, v.w, v.x);

        public static Vector4 xxwy(this Vector4 v) => new(v.x, v.x, v.w, v.y);

        public static Vector4 xxwz(this Vector4 v) => new(v.x, v.x, v.w, v.z);

        public static Vector4 xxww(this Vector4 v) => new(v.x, v.x, v.w, v.w);

        public static Vector4 xyxx(this Vector4 v) => new(v.x, v.y, v.x, v.x);

        public static Vector4 xyxy(this Vector4 v) => new(v.x, v.y, v.x, v.y);

        public static Vector4 xyxz(this Vector4 v) => new(v.x, v.y, v.x, v.z);

        public static Vector4 xyxw(this Vector4 v) => new(v.x, v.y, v.x, v.w);

        public static Vector4 xyyx(this Vector4 v) => new(v.x, v.y, v.y, v.x);

        public static Vector4 xyyy(this Vector4 v) => new(v.x, v.y, v.y, v.y);

        public static Vector4 xyyz(this Vector4 v) => new(v.x, v.y, v.y, v.z);

        public static Vector4 xyyw(this Vector4 v) => new(v.x, v.y, v.y, v.w);

        public static Vector4 xyzx(this Vector4 v) => new(v.x, v.y, v.z, v.x);

        public static Vector4 xyzy(this Vector4 v) => new(v.x, v.y, v.z, v.y);

        public static Vector4 xyzz(this Vector4 v) => new(v.x, v.y, v.z, v.z);

        public static Vector4 xyzw(this Vector4 v) => new(v.x, v.y, v.z, v.w);

        public static Vector4 xywx(this Vector4 v) => new(v.x, v.y, v.w, v.x);

        public static Vector4 xywy(this Vector4 v) => new(v.x, v.y, v.w, v.y);

        public static Vector4 xywz(this Vector4 v) => new(v.x, v.y, v.w, v.z);

        public static Vector4 xyww(this Vector4 v) => new(v.x, v.y, v.w, v.w);

        public static Vector4 xzxx(this Vector4 v) => new(v.x, v.z, v.x, v.x);

        public static Vector4 xzxy(this Vector4 v) => new(v.x, v.z, v.x, v.y);

        public static Vector4 xzxz(this Vector4 v) => new(v.x, v.z, v.x, v.z);

        public static Vector4 xzxw(this Vector4 v) => new(v.x, v.z, v.x, v.w);

        public static Vector4 xzyx(this Vector4 v) => new(v.x, v.z, v.y, v.x);

        public static Vector4 xzyy(this Vector4 v) => new(v.x, v.z, v.y, v.y);

        public static Vector4 xzyz(this Vector4 v) => new(v.x, v.z, v.y, v.z);

        public static Vector4 xzyw(this Vector4 v) => new(v.x, v.z, v.y, v.w);

        public static Vector4 xzzx(this Vector4 v) => new(v.x, v.z, v.z, v.x);

        public static Vector4 xzzy(this Vector4 v) => new(v.x, v.z, v.z, v.y);

        public static Vector4 xzzz(this Vector4 v) => new(v.x, v.z, v.z, v.z);

        public static Vector4 xzzw(this Vector4 v) => new(v.x, v.z, v.z, v.w);

        public static Vector4 xzwx(this Vector4 v) => new(v.x, v.z, v.w, v.x);

        public static Vector4 xzwy(this Vector4 v) => new(v.x, v.z, v.w, v.y);

        public static Vector4 xzwz(this Vector4 v) => new(v.x, v.z, v.w, v.z);

        public static Vector4 xzww(this Vector4 v) => new(v.x, v.z, v.w, v.w);

        public static Vector4 xwxx(this Vector4 v) => new(v.x, v.w, v.x, v.x);

        public static Vector4 xwxy(this Vector4 v) => new(v.x, v.w, v.x, v.y);

        public static Vector4 xwxz(this Vector4 v) => new(v.x, v.w, v.x, v.z);

        public static Vector4 xwxw(this Vector4 v) => new(v.x, v.w, v.x, v.w);

        public static Vector4 xwyx(this Vector4 v) => new(v.x, v.w, v.y, v.x);

        public static Vector4 xwyy(this Vector4 v) => new(v.x, v.w, v.y, v.y);

        public static Vector4 xwyz(this Vector4 v) => new(v.x, v.w, v.y, v.z);

        public static Vector4 xwyw(this Vector4 v) => new(v.x, v.w, v.y, v.w);

        public static Vector4 xwzx(this Vector4 v) => new(v.x, v.w, v.z, v.x);

        public static Vector4 xwzy(this Vector4 v) => new(v.x, v.w, v.z, v.y);

        public static Vector4 xwzz(this Vector4 v) => new(v.x, v.w, v.z, v.z);

        public static Vector4 xwzw(this Vector4 v) => new(v.x, v.w, v.z, v.w);

        public static Vector4 xwwx(this Vector4 v) => new(v.x, v.w, v.w, v.x);

        public static Vector4 xwwy(this Vector4 v) => new(v.x, v.w, v.w, v.y);

        public static Vector4 xwwz(this Vector4 v) => new(v.x, v.w, v.w, v.z);

        public static Vector4 xwww(this Vector4 v) => new(v.x, v.w, v.w, v.w);

        public static Vector4 yxxx(this Vector4 v) => new(v.y, v.x, v.x, v.x);

        public static Vector4 yxxy(this Vector4 v) => new(v.y, v.x, v.x, v.y);

        public static Vector4 yxxz(this Vector4 v) => new(v.y, v.x, v.x, v.z);

        public static Vector4 yxxw(this Vector4 v) => new(v.y, v.x, v.x, v.w);

        public static Vector4 yxyx(this Vector4 v) => new(v.y, v.x, v.y, v.x);

        public static Vector4 yxyy(this Vector4 v) => new(v.y, v.x, v.y, v.y);

        public static Vector4 yxyz(this Vector4 v) => new(v.y, v.x, v.y, v.z);

        public static Vector4 yxyw(this Vector4 v) => new(v.y, v.x, v.y, v.w);

        public static Vector4 yxzx(this Vector4 v) => new(v.y, v.x, v.z, v.x);

        public static Vector4 yxzy(this Vector4 v) => new(v.y, v.x, v.z, v.y);

        public static Vector4 yxzz(this Vector4 v) => new(v.y, v.x, v.z, v.z);

        public static Vector4 yxzw(this Vector4 v) => new(v.y, v.x, v.z, v.w);

        public static Vector4 yxwx(this Vector4 v) => new(v.y, v.x, v.w, v.x);

        public static Vector4 yxwy(this Vector4 v) => new(v.y, v.x, v.w, v.y);

        public static Vector4 yxwz(this Vector4 v) => new(v.y, v.x, v.w, v.z);

        public static Vector4 yxww(this Vector4 v) => new(v.y, v.x, v.w, v.w);

        public static Vector4 yyxx(this Vector4 v) => new(v.y, v.y, v.x, v.x);

        public static Vector4 yyxy(this Vector4 v) => new(v.y, v.y, v.x, v.y);

        public static Vector4 yyxz(this Vector4 v) => new(v.y, v.y, v.x, v.z);

        public static Vector4 yyxw(this Vector4 v) => new(v.y, v.y, v.x, v.w);

        public static Vector4 yyyx(this Vector4 v) => new(v.y, v.y, v.y, v.x);

        public static Vector4 yyyy(this Vector4 v) => new(v.y, v.y, v.y, v.y);

        public static Vector4 yyyz(this Vector4 v) => new(v.y, v.y, v.y, v.z);

        public static Vector4 yyyw(this Vector4 v) => new(v.y, v.y, v.y, v.w);

        public static Vector4 yyzx(this Vector4 v) => new(v.y, v.y, v.z, v.x);

        public static Vector4 yyzy(this Vector4 v) => new(v.y, v.y, v.z, v.y);

        public static Vector4 yyzz(this Vector4 v) => new(v.y, v.y, v.z, v.z);

        public static Vector4 yyzw(this Vector4 v) => new(v.y, v.y, v.z, v.w);

        public static Vector4 yywx(this Vector4 v) => new(v.y, v.y, v.w, v.x);

        public static Vector4 yywy(this Vector4 v) => new(v.y, v.y, v.w, v.y);

        public static Vector4 yywz(this Vector4 v) => new(v.y, v.y, v.w, v.z);

        public static Vector4 yyww(this Vector4 v) => new(v.y, v.y, v.w, v.w);

        public static Vector4 yzxx(this Vector4 v) => new(v.y, v.z, v.x, v.x);

        public static Vector4 yzxy(this Vector4 v) => new(v.y, v.z, v.x, v.y);

        public static Vector4 yzxz(this Vector4 v) => new(v.y, v.z, v.x, v.z);

        public static Vector4 yzxw(this Vector4 v) => new(v.y, v.z, v.x, v.w);

        public static Vector4 yzyx(this Vector4 v) => new(v.y, v.z, v.y, v.x);

        public static Vector4 yzyy(this Vector4 v) => new(v.y, v.z, v.y, v.y);

        public static Vector4 yzyz(this Vector4 v) => new(v.y, v.z, v.y, v.z);

        public static Vector4 yzyw(this Vector4 v) => new(v.y, v.z, v.y, v.w);

        public static Vector4 yzzx(this Vector4 v) => new(v.y, v.z, v.z, v.x);

        public static Vector4 yzzy(this Vector4 v) => new(v.y, v.z, v.z, v.y);

        public static Vector4 yzzz(this Vector4 v) => new(v.y, v.z, v.z, v.z);

        public static Vector4 yzzw(this Vector4 v) => new(v.y, v.z, v.z, v.w);

        public static Vector4 yzwx(this Vector4 v) => new(v.y, v.z, v.w, v.x);

        public static Vector4 yzwy(this Vector4 v) => new(v.y, v.z, v.w, v.y);

        public static Vector4 yzwz(this Vector4 v) => new(v.y, v.z, v.w, v.z);

        public static Vector4 yzww(this Vector4 v) => new(v.y, v.z, v.w, v.w);

        public static Vector4 ywxx(this Vector4 v) => new(v.y, v.w, v.x, v.x);

        public static Vector4 ywxy(this Vector4 v) => new(v.y, v.w, v.x, v.y);

        public static Vector4 ywxz(this Vector4 v) => new(v.y, v.w, v.x, v.z);

        public static Vector4 ywxw(this Vector4 v) => new(v.y, v.w, v.x, v.w);

        public static Vector4 ywyx(this Vector4 v) => new(v.y, v.w, v.y, v.x);

        public static Vector4 ywyy(this Vector4 v) => new(v.y, v.w, v.y, v.y);

        public static Vector4 ywyz(this Vector4 v) => new(v.y, v.w, v.y, v.z);

        public static Vector4 ywyw(this Vector4 v) => new(v.y, v.w, v.y, v.w);

        public static Vector4 ywzx(this Vector4 v) => new(v.y, v.w, v.z, v.x);

        public static Vector4 ywzy(this Vector4 v) => new(v.y, v.w, v.z, v.y);

        public static Vector4 ywzz(this Vector4 v) => new(v.y, v.w, v.z, v.z);

        public static Vector4 ywzw(this Vector4 v) => new(v.y, v.w, v.z, v.w);

        public static Vector4 ywwx(this Vector4 v) => new(v.y, v.w, v.w, v.x);

        public static Vector4 ywwy(this Vector4 v) => new(v.y, v.w, v.w, v.y);

        public static Vector4 ywwz(this Vector4 v) => new(v.y, v.w, v.w, v.z);

        public static Vector4 ywww(this Vector4 v) => new(v.y, v.w, v.w, v.w);

        public static Vector4 zxxx(this Vector4 v) => new(v.z, v.x, v.x, v.x);

        public static Vector4 zxxy(this Vector4 v) => new(v.z, v.x, v.x, v.y);

        public static Vector4 zxxz(this Vector4 v) => new(v.z, v.x, v.x, v.z);

        public static Vector4 zxxw(this Vector4 v) => new(v.z, v.x, v.x, v.w);

        public static Vector4 zxyx(this Vector4 v) => new(v.z, v.x, v.y, v.x);

        public static Vector4 zxyy(this Vector4 v) => new(v.z, v.x, v.y, v.y);

        public static Vector4 zxyz(this Vector4 v) => new(v.z, v.x, v.y, v.z);

        public static Vector4 zxyw(this Vector4 v) => new(v.z, v.x, v.y, v.w);

        public static Vector4 zxzx(this Vector4 v) => new(v.z, v.x, v.z, v.x);

        public static Vector4 zxzy(this Vector4 v) => new(v.z, v.x, v.z, v.y);

        public static Vector4 zxzz(this Vector4 v) => new(v.z, v.x, v.z, v.z);

        public static Vector4 zxzw(this Vector4 v) => new(v.z, v.x, v.z, v.w);

        public static Vector4 zxwx(this Vector4 v) => new(v.z, v.x, v.w, v.x);

        public static Vector4 zxwy(this Vector4 v) => new(v.z, v.x, v.w, v.y);

        public static Vector4 zxwz(this Vector4 v) => new(v.z, v.x, v.w, v.z);

        public static Vector4 zxww(this Vector4 v) => new(v.z, v.x, v.w, v.w);

        public static Vector4 zyxx(this Vector4 v) => new(v.z, v.y, v.x, v.x);

        public static Vector4 zyxy(this Vector4 v) => new(v.z, v.y, v.x, v.y);

        public static Vector4 zyxz(this Vector4 v) => new(v.z, v.y, v.x, v.z);

        public static Vector4 zyxw(this Vector4 v) => new(v.z, v.y, v.x, v.w);

        public static Vector4 zyyx(this Vector4 v) => new(v.z, v.y, v.y, v.x);

        public static Vector4 zyyy(this Vector4 v) => new(v.z, v.y, v.y, v.y);

        public static Vector4 zyyz(this Vector4 v) => new(v.z, v.y, v.y, v.z);

        public static Vector4 zyyw(this Vector4 v) => new(v.z, v.y, v.y, v.w);

        public static Vector4 zyzx(this Vector4 v) => new(v.z, v.y, v.z, v.x);

        public static Vector4 zyzy(this Vector4 v) => new(v.z, v.y, v.z, v.y);

        public static Vector4 zyzz(this Vector4 v) => new(v.z, v.y, v.z, v.z);

        public static Vector4 zyzw(this Vector4 v) => new(v.z, v.y, v.z, v.w);

        public static Vector4 zywx(this Vector4 v) => new(v.z, v.y, v.w, v.x);

        public static Vector4 zywy(this Vector4 v) => new(v.z, v.y, v.w, v.y);

        public static Vector4 zywz(this Vector4 v) => new(v.z, v.y, v.w, v.z);

        public static Vector4 zyww(this Vector4 v) => new(v.z, v.y, v.w, v.w);

        public static Vector4 zzxx(this Vector4 v) => new(v.z, v.z, v.x, v.x);

        public static Vector4 zzxy(this Vector4 v) => new(v.z, v.z, v.x, v.y);

        public static Vector4 zzxz(this Vector4 v) => new(v.z, v.z, v.x, v.z);

        public static Vector4 zzxw(this Vector4 v) => new(v.z, v.z, v.x, v.w);

        public static Vector4 zzyx(this Vector4 v) => new(v.z, v.z, v.y, v.x);

        public static Vector4 zzyy(this Vector4 v) => new(v.z, v.z, v.y, v.y);

        public static Vector4 zzyz(this Vector4 v) => new(v.z, v.z, v.y, v.z);

        public static Vector4 zzyw(this Vector4 v) => new(v.z, v.z, v.y, v.w);

        public static Vector4 zzzx(this Vector4 v) => new(v.z, v.z, v.z, v.x);

        public static Vector4 zzzy(this Vector4 v) => new(v.z, v.z, v.z, v.y);

        public static Vector4 zzzz(this Vector4 v) => new(v.z, v.z, v.z, v.z);

        public static Vector4 zzzw(this Vector4 v) => new(v.z, v.z, v.z, v.w);

        public static Vector4 zzwx(this Vector4 v) => new(v.z, v.z, v.w, v.x);

        public static Vector4 zzwy(this Vector4 v) => new(v.z, v.z, v.w, v.y);

        public static Vector4 zzwz(this Vector4 v) => new(v.z, v.z, v.w, v.z);

        public static Vector4 zzww(this Vector4 v) => new(v.z, v.z, v.w, v.w);

        public static Vector4 zwxx(this Vector4 v) => new(v.z, v.w, v.x, v.x);

        public static Vector4 zwxy(this Vector4 v) => new(v.z, v.w, v.x, v.y);

        public static Vector4 zwxz(this Vector4 v) => new(v.z, v.w, v.x, v.z);

        public static Vector4 zwxw(this Vector4 v) => new(v.z, v.w, v.x, v.w);

        public static Vector4 zwyx(this Vector4 v) => new(v.z, v.w, v.y, v.x);

        public static Vector4 zwyy(this Vector4 v) => new(v.z, v.w, v.y, v.y);

        public static Vector4 zwyz(this Vector4 v) => new(v.z, v.w, v.y, v.z);

        public static Vector4 zwyw(this Vector4 v) => new(v.z, v.w, v.y, v.w);

        public static Vector4 zwzx(this Vector4 v) => new(v.z, v.w, v.z, v.x);

        public static Vector4 zwzy(this Vector4 v) => new(v.z, v.w, v.z, v.y);

        public static Vector4 zwzz(this Vector4 v) => new(v.z, v.w, v.z, v.z);

        public static Vector4 zwzw(this Vector4 v) => new(v.z, v.w, v.z, v.w);

        public static Vector4 zwwx(this Vector4 v) => new(v.z, v.w, v.w, v.x);

        public static Vector4 zwwy(this Vector4 v) => new(v.z, v.w, v.w, v.y);

        public static Vector4 zwwz(this Vector4 v) => new(v.z, v.w, v.w, v.z);

        public static Vector4 zwww(this Vector4 v) => new(v.z, v.w, v.w, v.w);

        public static Vector4 wxxx(this Vector4 v) => new(v.w, v.x, v.x, v.x);

        public static Vector4 wxxy(this Vector4 v) => new(v.w, v.x, v.x, v.y);

        public static Vector4 wxxz(this Vector4 v) => new(v.w, v.x, v.x, v.z);

        public static Vector4 wxxw(this Vector4 v) => new(v.w, v.x, v.x, v.w);

        public static Vector4 wxyx(this Vector4 v) => new(v.w, v.x, v.y, v.x);

        public static Vector4 wxyy(this Vector4 v) => new(v.w, v.x, v.y, v.y);

        public static Vector4 wxyz(this Vector4 v) => new(v.w, v.x, v.y, v.z);

        public static Vector4 wxyw(this Vector4 v) => new(v.w, v.x, v.y, v.w);

        public static Vector4 wxzx(this Vector4 v) => new(v.w, v.x, v.z, v.x);

        public static Vector4 wxzy(this Vector4 v) => new(v.w, v.x, v.z, v.y);

        public static Vector4 wxzz(this Vector4 v) => new(v.w, v.x, v.z, v.z);

        public static Vector4 wxzw(this Vector4 v) => new(v.w, v.x, v.z, v.w);

        public static Vector4 wxwx(this Vector4 v) => new(v.w, v.x, v.w, v.x);

        public static Vector4 wxwy(this Vector4 v) => new(v.w, v.x, v.w, v.y);

        public static Vector4 wxwz(this Vector4 v) => new(v.w, v.x, v.w, v.z);

        public static Vector4 wxww(this Vector4 v) => new(v.w, v.x, v.w, v.w);

        public static Vector4 wyxx(this Vector4 v) => new(v.w, v.y, v.x, v.x);

        public static Vector4 wyxy(this Vector4 v) => new(v.w, v.y, v.x, v.y);

        public static Vector4 wyxz(this Vector4 v) => new(v.w, v.y, v.x, v.z);

        public static Vector4 wyxw(this Vector4 v) => new(v.w, v.y, v.x, v.w);

        public static Vector4 wyyx(this Vector4 v) => new(v.w, v.y, v.y, v.x);

        public static Vector4 wyyy(this Vector4 v) => new(v.w, v.y, v.y, v.y);

        public static Vector4 wyyz(this Vector4 v) => new(v.w, v.y, v.y, v.z);

        public static Vector4 wyyw(this Vector4 v) => new(v.w, v.y, v.y, v.w);

        public static Vector4 wyzx(this Vector4 v) => new(v.w, v.y, v.z, v.x);

        public static Vector4 wyzy(this Vector4 v) => new(v.w, v.y, v.z, v.y);

        public static Vector4 wyzz(this Vector4 v) => new(v.w, v.y, v.z, v.z);

        public static Vector4 wyzw(this Vector4 v) => new(v.w, v.y, v.z, v.w);

        public static Vector4 wywx(this Vector4 v) => new(v.w, v.y, v.w, v.x);

        public static Vector4 wywy(this Vector4 v) => new(v.w, v.y, v.w, v.y);

        public static Vector4 wywz(this Vector4 v) => new(v.w, v.y, v.w, v.z);

        public static Vector4 wyww(this Vector4 v) => new(v.w, v.y, v.w, v.w);

        public static Vector4 wzxx(this Vector4 v) => new(v.w, v.z, v.x, v.x);

        public static Vector4 wzxy(this Vector4 v) => new(v.w, v.z, v.x, v.y);

        public static Vector4 wzxz(this Vector4 v) => new(v.w, v.z, v.x, v.z);

        public static Vector4 wzxw(this Vector4 v) => new(v.w, v.z, v.x, v.w);

        public static Vector4 wzyx(this Vector4 v) => new(v.w, v.z, v.y, v.x);

        public static Vector4 wzyy(this Vector4 v) => new(v.w, v.z, v.y, v.y);

        public static Vector4 wzyz(this Vector4 v) => new(v.w, v.z, v.y, v.z);

        public static Vector4 wzyw(this Vector4 v) => new(v.w, v.z, v.y, v.w);

        public static Vector4 wzzx(this Vector4 v) => new(v.w, v.z, v.z, v.x);

        public static Vector4 wzzy(this Vector4 v) => new(v.w, v.z, v.z, v.y);

        public static Vector4 wzzz(this Vector4 v) => new(v.w, v.z, v.z, v.z);

        public static Vector4 wzzw(this Vector4 v) => new(v.w, v.z, v.z, v.w);

        public static Vector4 wzwx(this Vector4 v) => new(v.w, v.z, v.w, v.x);

        public static Vector4 wzwy(this Vector4 v) => new(v.w, v.z, v.w, v.y);

        public static Vector4 wzwz(this Vector4 v) => new(v.w, v.z, v.w, v.z);

        public static Vector4 wzww(this Vector4 v) => new(v.w, v.z, v.w, v.w);

        public static Vector4 wwxx(this Vector4 v) => new(v.w, v.w, v.x, v.x);

        public static Vector4 wwxy(this Vector4 v) => new(v.w, v.w, v.x, v.y);

        public static Vector4 wwxz(this Vector4 v) => new(v.w, v.w, v.x, v.z);

        public static Vector4 wwxw(this Vector4 v) => new(v.w, v.w, v.x, v.w);

        public static Vector4 wwyx(this Vector4 v) => new(v.w, v.w, v.y, v.x);

        public static Vector4 wwyy(this Vector4 v) => new(v.w, v.w, v.y, v.y);

        public static Vector4 wwyz(this Vector4 v) => new(v.w, v.w, v.y, v.z);

        public static Vector4 wwyw(this Vector4 v) => new(v.w, v.w, v.y, v.w);

        public static Vector4 wwzx(this Vector4 v) => new(v.w, v.w, v.z, v.x);

        public static Vector4 wwzy(this Vector4 v) => new(v.w, v.w, v.z, v.y);

        public static Vector4 wwzz(this Vector4 v) => new(v.w, v.w, v.z, v.z);

        public static Vector4 wwzw(this Vector4 v) => new(v.w, v.w, v.z, v.w);

        public static Vector4 wwwx(this Vector4 v) => new(v.w, v.w, v.w, v.x);

        public static Vector4 wwwy(this Vector4 v) => new(v.w, v.w, v.w, v.y);

        public static Vector4 wwwz(this Vector4 v) => new(v.w, v.w, v.w, v.z);

        public static Vector4 wwww(this Vector4 v) => new(v.w, v.w, v.w, v.w);

        public static Vector2 xx(this Vector3 v) => new(v.x, v.x);

        public static Vector2 xy(this Vector3 v) => new(v.x, v.y);

        public static Vector2 xz(this Vector3 v) => new(v.x, v.z);

        public static Vector2 yx(this Vector3 v) => new(v.y, v.x);

        public static Vector2 yy(this Vector3 v) => new(v.y, v.y);

        public static Vector2 yz(this Vector3 v) => new(v.y, v.z);

        public static Vector2 zx(this Vector3 v) => new(v.z, v.x);

        public static Vector2 zy(this Vector3 v) => new(v.z, v.y);

        public static Vector2 zz(this Vector3 v) => new(v.z, v.z);

        public static Vector3 xxx(this Vector3 v) => new(v.x, v.x, v.x);

        public static Vector3 xxy(this Vector3 v) => new(v.x, v.x, v.y);

        public static Vector3 xxz(this Vector3 v) => new(v.x, v.x, v.z);

        public static Vector3 xyx(this Vector3 v) => new(v.x, v.y, v.x);

        public static Vector3 xyy(this Vector3 v) => new(v.x, v.y, v.y);

        public static Vector3 xyz(this Vector3 v) => new(v.x, v.y, v.z);

        public static Vector3 xzx(this Vector3 v) => new(v.x, v.z, v.x);

        public static Vector3 xzy(this Vector3 v) => new(v.x, v.z, v.y);

        public static Vector3 xzz(this Vector3 v) => new(v.x, v.z, v.z);

        public static Vector3 yxx(this Vector3 v) => new(v.y, v.x, v.x);

        public static Vector3 yxy(this Vector3 v) => new(v.y, v.x, v.y);

        public static Vector3 yxz(this Vector3 v) => new(v.y, v.x, v.z);

        public static Vector3 yyx(this Vector3 v) => new(v.y, v.y, v.x);

        public static Vector3 yyy(this Vector3 v) => new(v.y, v.y, v.y);

        public static Vector3 yyz(this Vector3 v) => new(v.y, v.y, v.z);

        public static Vector3 yzx(this Vector3 v) => new(v.y, v.z, v.x);

        public static Vector3 yzy(this Vector3 v) => new(v.y, v.z, v.y);

        public static Vector3 yzz(this Vector3 v) => new(v.y, v.z, v.z);

        public static Vector3 zxx(this Vector3 v) => new(v.z, v.x, v.x);

        public static Vector3 zxy(this Vector3 v) => new(v.z, v.x, v.y);

        public static Vector3 zxz(this Vector3 v) => new(v.z, v.x, v.z);

        public static Vector3 zyx(this Vector3 v) => new(v.z, v.y, v.x);

        public static Vector3 zyy(this Vector3 v) => new(v.z, v.y, v.y);

        public static Vector3 zyz(this Vector3 v) => new(v.z, v.y, v.z);

        public static Vector3 zzx(this Vector3 v) => new(v.z, v.z, v.x);

        public static Vector3 zzy(this Vector3 v) => new(v.z, v.z, v.y);

        public static Vector3 zzz(this Vector3 v) => new(v.z, v.z, v.z);

        public static Vector4 xxxx(this Vector3 v) => new(v.x, v.x, v.x, v.x);

        public static Vector4 xxxy(this Vector3 v) => new(v.x, v.x, v.x, v.y);

        public static Vector4 xxxz(this Vector3 v) => new(v.x, v.x, v.x, v.z);

        public static Vector4 xxyx(this Vector3 v) => new(v.x, v.x, v.y, v.x);

        public static Vector4 xxyy(this Vector3 v) => new(v.x, v.x, v.y, v.y);

        public static Vector4 xxyz(this Vector3 v) => new(v.x, v.x, v.y, v.z);

        public static Vector4 xxzx(this Vector3 v) => new(v.x, v.x, v.z, v.x);

        public static Vector4 xxzy(this Vector3 v) => new(v.x, v.x, v.z, v.y);

        public static Vector4 xxzz(this Vector3 v) => new(v.x, v.x, v.z, v.z);

        public static Vector4 xyxx(this Vector3 v) => new(v.x, v.y, v.x, v.x);

        public static Vector4 xyxy(this Vector3 v) => new(v.x, v.y, v.x, v.y);

        public static Vector4 xyxz(this Vector3 v) => new(v.x, v.y, v.x, v.z);

        public static Vector4 xyyx(this Vector3 v) => new(v.x, v.y, v.y, v.x);

        public static Vector4 xyyy(this Vector3 v) => new(v.x, v.y, v.y, v.y);

        public static Vector4 xyyz(this Vector3 v) => new(v.x, v.y, v.y, v.z);

        public static Vector4 xyzx(this Vector3 v) => new(v.x, v.y, v.z, v.x);

        public static Vector4 xyzy(this Vector3 v) => new(v.x, v.y, v.z, v.y);

        public static Vector4 xyzz(this Vector3 v) => new(v.x, v.y, v.z, v.z);

        public static Vector4 xzxx(this Vector3 v) => new(v.x, v.z, v.x, v.x);

        public static Vector4 xzxy(this Vector3 v) => new(v.x, v.z, v.x, v.y);

        public static Vector4 xzxz(this Vector3 v) => new(v.x, v.z, v.x, v.z);

        public static Vector4 xzyx(this Vector3 v) => new(v.x, v.z, v.y, v.x);

        public static Vector4 xzyy(this Vector3 v) => new(v.x, v.z, v.y, v.y);

        public static Vector4 xzyz(this Vector3 v) => new(v.x, v.z, v.y, v.z);

        public static Vector4 xzzx(this Vector3 v) => new(v.x, v.z, v.z, v.x);

        public static Vector4 xzzy(this Vector3 v) => new(v.x, v.z, v.z, v.y);

        public static Vector4 xzzz(this Vector3 v) => new(v.x, v.z, v.z, v.z);

        public static Vector4 yxxx(this Vector3 v) => new(v.y, v.x, v.x, v.x);

        public static Vector4 yxxy(this Vector3 v) => new(v.y, v.x, v.x, v.y);

        public static Vector4 yxxz(this Vector3 v) => new(v.y, v.x, v.x, v.z);

        public static Vector4 yxyx(this Vector3 v) => new(v.y, v.x, v.y, v.x);

        public static Vector4 yxyy(this Vector3 v) => new(v.y, v.x, v.y, v.y);

        public static Vector4 yxyz(this Vector3 v) => new(v.y, v.x, v.y, v.z);

        public static Vector4 yxzx(this Vector3 v) => new(v.y, v.x, v.z, v.x);

        public static Vector4 yxzy(this Vector3 v) => new(v.y, v.x, v.z, v.y);

        public static Vector4 yxzz(this Vector3 v) => new(v.y, v.x, v.z, v.z);

        public static Vector4 yyxx(this Vector3 v) => new(v.y, v.y, v.x, v.x);

        public static Vector4 yyxy(this Vector3 v) => new(v.y, v.y, v.x, v.y);

        public static Vector4 yyxz(this Vector3 v) => new(v.y, v.y, v.x, v.z);

        public static Vector4 yyyx(this Vector3 v) => new(v.y, v.y, v.y, v.x);

        public static Vector4 yyyy(this Vector3 v) => new(v.y, v.y, v.y, v.y);

        public static Vector4 yyyz(this Vector3 v) => new(v.y, v.y, v.y, v.z);

        public static Vector4 yyzx(this Vector3 v) => new(v.y, v.y, v.z, v.x);

        public static Vector4 yyzy(this Vector3 v) => new(v.y, v.y, v.z, v.y);

        public static Vector4 yyzz(this Vector3 v) => new(v.y, v.y, v.z, v.z);

        public static Vector4 yzxx(this Vector3 v) => new(v.y, v.z, v.x, v.x);

        public static Vector4 yzxy(this Vector3 v) => new(v.y, v.z, v.x, v.y);

        public static Vector4 yzxz(this Vector3 v) => new(v.y, v.z, v.x, v.z);

        public static Vector4 yzyx(this Vector3 v) => new(v.y, v.z, v.y, v.x);

        public static Vector4 yzyy(this Vector3 v) => new(v.y, v.z, v.y, v.y);

        public static Vector4 yzyz(this Vector3 v) => new(v.y, v.z, v.y, v.z);

        public static Vector4 yzzx(this Vector3 v) => new(v.y, v.z, v.z, v.x);

        public static Vector4 yzzy(this Vector3 v) => new(v.y, v.z, v.z, v.y);

        public static Vector4 yzzz(this Vector3 v) => new(v.y, v.z, v.z, v.z);

        public static Vector4 zxxx(this Vector3 v) => new(v.z, v.x, v.x, v.x);

        public static Vector4 zxxy(this Vector3 v) => new(v.z, v.x, v.x, v.y);

        public static Vector4 zxxz(this Vector3 v) => new(v.z, v.x, v.x, v.z);

        public static Vector4 zxyx(this Vector3 v) => new(v.z, v.x, v.y, v.x);

        public static Vector4 zxyy(this Vector3 v) => new(v.z, v.x, v.y, v.y);

        public static Vector4 zxyz(this Vector3 v) => new(v.z, v.x, v.y, v.z);

        public static Vector4 zxzx(this Vector3 v) => new(v.z, v.x, v.z, v.x);

        public static Vector4 zxzy(this Vector3 v) => new(v.z, v.x, v.z, v.y);

        public static Vector4 zxzz(this Vector3 v) => new(v.z, v.x, v.z, v.z);

        public static Vector4 zyxx(this Vector3 v) => new(v.z, v.y, v.x, v.x);

        public static Vector4 zyxy(this Vector3 v) => new(v.z, v.y, v.x, v.y);

        public static Vector4 zyxz(this Vector3 v) => new(v.z, v.y, v.x, v.z);

        public static Vector4 zyyx(this Vector3 v) => new(v.z, v.y, v.y, v.x);

        public static Vector4 zyyy(this Vector3 v) => new(v.z, v.y, v.y, v.y);

        public static Vector4 zyyz(this Vector3 v) => new(v.z, v.y, v.y, v.z);

        public static Vector4 zyzx(this Vector3 v) => new(v.z, v.y, v.z, v.x);

        public static Vector4 zyzy(this Vector3 v) => new(v.z, v.y, v.z, v.y);

        public static Vector4 zyzz(this Vector3 v) => new(v.z, v.y, v.z, v.z);

        public static Vector4 zzxx(this Vector3 v) => new(v.z, v.z, v.x, v.x);

        public static Vector4 zzxy(this Vector3 v) => new(v.z, v.z, v.x, v.y);

        public static Vector4 zzxz(this Vector3 v) => new(v.z, v.z, v.x, v.z);

        public static Vector4 zzyx(this Vector3 v) => new(v.z, v.z, v.y, v.x);

        public static Vector4 zzyy(this Vector3 v) => new(v.z, v.z, v.y, v.y);

        public static Vector4 zzyz(this Vector3 v) => new(v.z, v.z, v.y, v.z);

        public static Vector4 zzzx(this Vector3 v) => new(v.z, v.z, v.z, v.x);

        public static Vector4 zzzy(this Vector3 v) => new(v.z, v.z, v.z, v.y);

        public static Vector4 zzzz(this Vector3 v) => new(v.z, v.z, v.z, v.z);

        public static Vector2 xx(this Vector2 v) => new(v.x, v.x);

        public static Vector2 xy(this Vector2 v) => new(v.x, v.y);

        public static Vector2 yx(this Vector2 v) => new(v.y, v.x);

        public static Vector2 yy(this Vector2 v) => new(v.y, v.y);

        public static Vector3 xxx(this Vector2 v) => new(v.x, v.x, v.x);

        public static Vector3 xxy(this Vector2 v) => new(v.x, v.x, v.y);

        public static Vector3 xyx(this Vector2 v) => new(v.x, v.y, v.x);

        public static Vector3 xyy(this Vector2 v) => new(v.x, v.y, v.y);

        public static Vector3 yxx(this Vector2 v) => new(v.y, v.x, v.x);

        public static Vector3 yxy(this Vector2 v) => new(v.y, v.x, v.y);

        public static Vector3 yyx(this Vector2 v) => new(v.y, v.y, v.x);

        public static Vector3 yyy(this Vector2 v) => new(v.y, v.y, v.y);

        public static Vector4 xxxx(this Vector2 v) => new(v.x, v.x, v.x, v.x);

        public static Vector4 xxxy(this Vector2 v) => new(v.x, v.x, v.x, v.y);

        public static Vector4 xxyx(this Vector2 v) => new(v.x, v.x, v.y, v.x);

        public static Vector4 xxyy(this Vector2 v) => new(v.x, v.x, v.y, v.y);

        public static Vector4 xyxx(this Vector2 v) => new(v.x, v.y, v.x, v.x);

        public static Vector4 xyxy(this Vector2 v) => new(v.x, v.y, v.x, v.y);

        public static Vector4 xyyx(this Vector2 v) => new(v.x, v.y, v.y, v.x);

        public static Vector4 xyyy(this Vector2 v) => new(v.x, v.y, v.y, v.y);

        public static Vector4 yxxx(this Vector2 v) => new(v.y, v.x, v.x, v.x);

        public static Vector4 yxxy(this Vector2 v) => new(v.y, v.x, v.x, v.y);

        public static Vector4 yxyx(this Vector2 v) => new(v.y, v.x, v.y, v.x);

        public static Vector4 yxyy(this Vector2 v) => new(v.y, v.x, v.y, v.y);

        public static Vector4 yyxx(this Vector2 v) => new(v.y, v.y, v.x, v.x);

        public static Vector4 yyxy(this Vector2 v) => new(v.y, v.y, v.x, v.y);

        public static Vector4 yyyx(this Vector2 v) => new(v.y, v.y, v.y, v.x);

        public static Vector4 yyyy(this Vector2 v) => new(v.y, v.y, v.y, v.y);

        public static Vector2 xx(this float x) => new(x, x);

        public static Vector3 xxx(this float x) => new(x, x, x);

        public static Vector4 xxxx(this float x) => new(x, x, x, x);

        public static Vector2 xx(this int x) => new(x, x);

        public static Vector3 xxx(this int x) => new(x, x, x);

        public static Vector4 xxxx(this int x) => new(x, x, x, x);
    }
}