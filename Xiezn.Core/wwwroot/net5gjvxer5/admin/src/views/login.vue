<template>
  <div>
    <div class="container" :style='{"minHeight":"100vh","alignItems":"center","background":"url(http://codegen.caihongy.cn/20230830/c15326488e814048bf3e12fa1253456c.jpg)","display":"flex","width":"100%","backgroundSize":"cover","backgroundPosition":"center center","backgroundRepeat":"no-repeat","justifyContent":"center"}'>
      <el-form :style='{"border":"0px solid #eee","padding":"20px 20px 120px 20px","boxShadow":"0 1px 9px rgba(64, 158, 255, .6)","margin":"0","borderRadius":"0px","textAlign":"center","background":"rgba(255,255,255,1)","clipPath":"polygon(0% 0%, 100% 0%, 100% 85%, 75% 85%, 75% 100%, 50% 85%, 0 85%)","width":"750px","fontSize":"16px","height":"auto"}'>
        <div v-if="true" :style='{"padding":"10px","margin":"0px auto 30px","color":"rgba(55,55,55,1)","textAlign":"center","background":"linear-gradient(90deg, rgba(22,136,199,1) 0%, rgba(54,221,227,1) 50%, rgba(140,179,122,1) 100%)","-webkitTextFillColor":"transparent","display":"inline-block","width":"auto","lineHeight":"40px","fontSize":"28px","fontWeight":"600","-webkitBackgroundClip":"text"}' class="title-container">基于ASP.NET的视频点播系统的设计与实现登录</div>
        <div v-if="loginType==1" class="list-item" :style='{"width":"65%","margin":"0 auto 20px","position":"relative","alignItems":"center","flexWrap":"wrap","display":"flex"}'>
          <div v-if="true" class="lable" :style='{"color":"#666","left":"-150px","textAlign":"right","width":"150px","lineHeight":"44px","fontSize":"inherit","position":"absolute"}'>用户名：</div>
          <input :style='{"border":"1px solid rgba(19,161,230,.2)","padding":"0 10px","boxShadow":"inset 0px 0px 24px 0px rgba(19,161,230,.1)","outline":"0px solid #efefef","color":"#666","outlineOffset":"0px","borderRadius":"0px","background":"#fff","width":"100%","fontSize":"inherit","height":"40px"}' placeholder="请输入用户名" name="username" type="text" v-model="rulesForm.username">
        </div>
        <div v-if="loginType==1" class="list-item" :style='{"width":"65%","margin":"0 auto 20px","position":"relative","alignItems":"center","flexWrap":"wrap","display":"flex"}'>
          <div v-if="true" class="lable" :style='{"color":"#666","left":"-150px","textAlign":"right","width":"150px","lineHeight":"44px","fontSize":"inherit","position":"absolute"}'>密码：</div>
          <input :style='{"border":"1px solid rgba(19,161,230,.2)","padding":"0 10px","boxShadow":"inset 0px 0px 24px 0px rgba(19,161,230,.1)","outline":"0px solid #efefef","color":"#666","outlineOffset":"0px","borderRadius":"0px","background":"#fff","width":"100%","fontSize":"inherit","height":"40px"}' placeholder="请输入密码" name="password" type="password" v-model="rulesForm.password">
        </div>

        <div :style='{"width":"65%","margin":"20px auto","fontSize":"inherit","textAlign":"left"}' v-if="roles.length>1" prop="loginInRole" class="list-type">
          <el-radio v-if="loginType==1||(loginType==2&&item.roleName!='管理员')" v-for="item in roles" v-bind:key="item.roleName" v-model="rulesForm.role" :label="item.roleName">{{item.roleName}}</el-radio>
        </div>

		
        <div :style='{"margin":"40px auto 0","alignItems":"center","flexWrap":"wrap","display":"flex","width":"88%","fontSize":"16px","justifyContent":"center"}'>
          <el-button v-if="loginType==1" :style='{"border":"1px solid rgba(19,161,230,.2)","cursor":"pointer","padding":"6px 10px","boxShadow":"inset 0px 0px 12px 0px rgba(19,161,230,.1)","margin":"0 5px 5px","color":"rgba(19,161,230,1)","outline":"none","borderRadius":"0px","background":"rgba(3, 152, 15, .0)","width":"auto","fontSize":"inherit","fontWeight":"600","height":"auto"}' type="primary" @click="login()" class="loginInBt">登录</el-button>
        </div>
      </el-form>

    </div>
  </div>
</template>
<script>
import menu from "@/utils/menu";
export default {
  data() {
    return {
		verifyCheck2: false,
		flag: false,
      baseUrl:this.$base.url,
      loginType: 1,
      rulesForm: {
        username: "",
        password: "",
        role: "",
      },
      menus: [],
      roles: [],
      tableName: "",
    };
  },
  mounted() {
    let menus = menu.list();
    this.menus = menus;

    for (let i = 0; i < this.menus.length; i++) {
      if (this.menus[i].hasBackLogin=='是') {
        this.roles.push(this.menus[i])
      }
    }

  },
  created() {

  },
  destroyed() {
	    },
  components: {
  },
  methods: {

    //注册
    register(tableName){
		this.$storage.set("loginTable", tableName);
		this.$router.push({path:'/register',query:{pageFlag:'register'}})
    },
    // 登陆
    login() {

		if (!this.rulesForm.username) {
			this.$message.error("请输入用户名");
			return;
		}
		if (!this.rulesForm.password) {
			this.$message.error("请输入密码");
			return;
		}
		if(this.roles.length>1) {
			if (!this.rulesForm.role) {
				this.$message.error("请选择角色");
				return;
			}

			let menus = this.menus;
			for (let i = 0; i < menus.length; i++) {
				if (menus[i].roleName == this.rulesForm.role) {
					this.tableName = menus[i].tableName;
				}
			}
		} else {
			this.tableName = this.roles[0].tableName;
			this.rulesForm.role = this.roles[0].roleName;
		}
		
		this.loginPost()
    },
	loginPost() {
		this.$http({
			url: `${this.tableName}/login?username=${this.rulesForm.username}&password=${this.rulesForm.password}`,
			method: "post"
		}).then(({ data }) => {
			if (data && data.code === 0) {
				this.$storage.set("Token", data.token);
				this.$storage.set("role", this.rulesForm.role);
				this.$storage.set("sessionTable", this.tableName);
				this.$storage.set("adminName", this.rulesForm.username);
				this.$router.replace({ path: "/" });
			} else {
				this.$message.error(data.msg);
			}
		});
	},
  }
}
</script>

<style lang="scss" scoped>
.container {
  min-height: 100vh;
  position: relative;
  background-repeat: no-repeat;
  background-position: center center;
  background-size: cover;
      background: url(http://codegen.caihongy.cn/20230830/c15326488e814048bf3e12fa1253456c.jpg);
        
  .list-item /deep/ .el-input .el-input__inner {
		border: 1px solid rgba(19,161,230,.2);
		border-radius: 0px;
		padding: 0 10px;
		box-shadow: inset 0px 0px 24px 0px rgba(19,161,230,.1);
		outline: 0px solid #efefef;
		color: #666;
		background: #fff;
		width: 100%;
		font-size: inherit;
		outline-offset: 0px;
		height: 40px;
	  }
  
  .list-item.select /deep/ .el-select .el-input__inner {
		border: 1px solid rgba(64, 158, 255, 1);
		padding: 0 10px;
		box-shadow: 0 0 6px rgba(64, 158, 255, .5);
		outline: 1px solid #efefef;
		color: rgba(64, 158, 255, 1);
		width: 288px;
		font-size: 14px;
		outline-offset: 4px;
		height: 44px;
	  }
  
  .list-code /deep/ .el-input .el-input__inner {
  	  	border: 1px solid rgba(19,161,230,.2);
  	  	border-radius: 0px 0 0 0px;
  	  	padding: 0 10px;
  	  	box-shadow: inset 0px 0px 24px 0px rgba(19,161,230,.1);
  	  	outline: none;
  	  	color: #666;
  	  	background: #fff;
  	  	width: calc(100% - 80px);
  	  	font-size: inherit;
  	  	height: 40px;
  	  }

  .list-type /deep/ .el-radio__input .el-radio__inner {
		background: rgba(53, 53, 53, 0);
		border-color: #999;
	  }
  .list-type /deep/ .el-radio__input.is-checked .el-radio__inner {
        background: rgba(19,161,230,.2);
        border-color: rgba(19,161,230,.3);
      }
  .list-type /deep/ .el-radio__label {
		color: #666666;
		font-size: 16px;
	  }
  .list-type /deep/ .el-radio__input.is-checked+.el-radio__label {
        color: rgba(50,50,50,1);
        font-size: 16px;
      }
}

</style>
