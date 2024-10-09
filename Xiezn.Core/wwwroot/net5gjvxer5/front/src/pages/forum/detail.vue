<template>
  <div :style='{"width":"100%","padding":"20px 12%","margin":"0 auto","position":"relative","background":"#edeff1"}'>
    <div :style='{"width":"100%","padding":"12px 0 0","margin":"0 auto","textAlign":"center","background":"none","height":"60px"}'>
		<div :style='{"padding":"0 20px 0 0","color":"#333","textAlign":"left","background":"none","display":"inline-block","width":"auto","fontSize":"22px","fontWeight":"600"}'>在线交流</div>
	</div>
	<div :style='{"padding":"16px 12%","margin":"0px auto","borderColor":"#d8d8d8","borderRadius":"0px","background":"none","borderWidth":"0 0 3px","width":"100%","borderStyle":"solid"}'>
		<el-button size="mini" @click="backClick">返回</el-button>
	</div>
    <div class="section-content" :style='{"border":"0px solid #f6f6f6","width":"100%","padding":"40px 40px 120px","margin":"20px auto","position":"relative","background":"#fff"}'>
      <div class="content-title" :style='{"padding":"0 0px","margin":"0 0 10px","color":"#333","textAlign":"center","width":"100%","lineHeight":"32px","fontSize":"18px","fontWeight":"600","height":"32px"}'>{{detail.title}}</div>
	  <div :style='{"width":"100%","alignItems":"center","justifyContent":"flex-end","display":"flex"}'>
	    <div :style='{"margin":"0 10px 0 0","fontSize":"14px","color":"#999","textAlign":"right"}'>发布人：{{detail.username}}</div>
	    <div :style='{"color":"#999","textAlign":"left","fontSize":"14px"}'>发布时间：{{detail.addtime}}</div>
	  </div>
	  <div class="operate" :style='{"padding":"0px","alignItems":"center","borderRadius":"0px","textAlign":"right","left":"47.3%","background":"#3795c4","bottom":"40px","display":"inline-block","width":"auto","position":"absolute","justifyContent":"flex-end"}'>
	    <div @click="zanClick" :style='{"border":"0px solid #1abc9e20","cursor":"pointer","padding":"0 10px","margin":"0 20px","alignItems":"center","background":"none","display":"flex","justifyContent":"center"}'>
	      <span class="icon iconfont icon-zan19" :style='{"margin":"0 6px 0 0","lineHeight":"30px","fontSize":"16px","color":"#fff","display":"none"}'></span>
	      <span :style='{"color":"#fff","lineHeight":"36px","fontSize":"14px"}'>{{zanType?'取消赞':'点赞'}}({{allZan}})</span>
	    </div>
	  </div>
      <div :style='{"border":"0px solid #ddd","padding":"20px 0px","fontSize":"14px","color":"#999"}' class="content-detail" v-html="detail.content"></div>
      
	  
		<div :style='{"border":"0px solid #eee","width":"100%","padding":"0","margin":"20px auto","background":"none"}'>
			<div :style='{"padding":"0 0 10px 0","borderColor":"#d8d8d8","alignItems":"center","borderWidth":"0 0 3px 0","display":"flex","width":"100%","position":"relative","borderStyle":"solid"}'>
				<div :style='{"color":"#333","fontSize":"18px","fontWeight":"600"}'>评论列表</div>
				<el-button class="pub" type="primary" :style='{"cursor":"pointer","border":"1px solid #1abc9e20","padding":"0px 30px","margin":"0 10px 0 40px","outline":"none","color":"#fff","borderRadius":"0px","background":"#3795c4","width":"auto","fontSize":"14px","lineHeight":"44px","height":"44px"}' @click="addComment">
					<span class="icon iconfont icon-xiugai10" :style='{"margin":"0 4px 0 0","fontSize":"14px","color":"#fff","display":"none"}'></span>
					点击评论
				</el-button>
			</div>
			<div class="comment" :style='{"width":"100%","padding":"20px 0 0","background":"none"}'>
				<template v-if="commentList && commentList.length">
				<div class="item" :style='{"border":"0px solid #1e3c4f","padding":"20px 20px 30px","margin":"20px 0 40px","alignItems":"center","color":"#666","background":"#fff","width":"100%","height":"auto"}' v-for="item in commentList" :key="item.id" @mouseenter="commentEnter(item.id)" @mouseleave="commentLeave">
					<div class="user" :style='{"border":"0px solid #f6f6f6","padding":"4px","alignItems":"center","borderRadius":"4px","background":"#fff","display":"flex","width":"100%","height":"auto"}'>
						<img v-if="item.avatarurl" :style='{"width":"36px","margin":"0 10px 0 0","borderRadius":"100%","objectFit":"cover","height":"36px"}' :src="baseUrl + item.avatarurl">
						<img v-if="!item.avatarurl" :style='{"width":"36px","margin":"0 10px 0 0","borderRadius":"100%","objectFit":"cover","height":"36px"}' :src="require('@/assets/touxiang.png')">
						<div :style='{"color":"#666","fontSize":"16px"}' class="name">用户：{{item.username}}</div>
					</div>
					<div :style='{"border":"0px solid #eee","padding":"10px","margin":"10px 0px 0px","color":"#666","borderRadius":"4px","background":"none","lineHeight":"24px","fontSize":"14px"}' v-html="item.content"></div>
					<div class="btn" :style='{"width":"100%","margin":"12px 0 0 0","alignItems":"center","justifyContent":"flex-start","display":"flex","height":"auto"}'>
						<el-button :style='{"border":"0px solid #ddd","cursor":"pointer","padding":"0 20px","margin":"0 10px","outline":"none","color":"#fff","borderRadius":"20px","background":"#3795c4","width":"auto","lineHeight":"30px","fontSize":"14px","height":"30px"}' v-if="showIndex==item.id&&showIndex1 == -1" @click="replyClick(item.id)">回复</el-button>
						<el-button :style='{"border":"1px solid #3795c4","cursor":"pointer","padding":"0 20px","margin":"0 10px","outline":"none","color":"#3795c4","borderRadius":"20px","background":"none","width":"auto","lineHeight":"30px","fontSize":"14px","height":"30px"}' v-if="showIndex==item.id&&userid==item.userid&&showIndex1 == -1" @click="commentDel(item.id)">删除</el-button>
					</div>
					
					<template v-if="item.childs && item.childs.length">
					<div class="comment"  :style='{"width":"100%","padding":"0 0px"}'>
						<div class="item" :style='{"padding":"10px","margin":"20px 0 0 0","borderColor":"#edeff1","alignItems":"center","color":"#666","background":"#fff","borderWidth":"2px 0 0","width":"100%","borderStyle":"solid","height":"auto"}' v-for="items in item.childs" :key="items.id" @mouseenter="commentEnter1(items.id)" @mouseleave="commentLeave1">
							<div class="user" :style='{"border":"0px solid #f6f6f6","padding":"4px","alignItems":"center","borderRadius":"4px","background":"none","display":"flex","width":"100%","height":"auto"}'>
								<img v-if="items.avatarurl" :style='{"width":"36px","margin":"0 10px 0 0","borderRadius":"100%","objectFit":"cover","height":"36px"}' :src="baseUrl + items.avatarurl">
								<img v-if="!items.avatarurl" :style='{"width":"36px","margin":"0 10px 0 0","borderRadius":"100%","objectFit":"cover","height":"36px"}' :src="require('@/assets/touxiang.png')">
								<div :style='{"color":"#333","fontSize":"16px"}' class="name">用户：{{items.username}}</div>
							</div>
							<div :style='{"padding":"0px","margin":"4px 0 0","color":"#666","borderRadius":"4px","background":"none","lineHeight":"30px","fontSize":"14px","textIndent":"2em"}' v-html="items.content"></div>
							<div class="btn" :style='{"width":"100%","margin":"10px 0 0 0","alignItems":"center","justifyContent":"flex-start","display":"flex","height":"auto"}'>
								<el-button :style='{"border":"1px solid #3795c4","cursor":"pointer","padding":"0 20px","margin":"0 10px","outline":"none","color":"#3795c4","borderRadius":"20px","background":"none","width":"auto","lineHeight":"30px","fontSize":"14px","height":"30px"}' v-if="showIndex==item.id&&userid==items.userid&&showIndex1==items.id" @click="commentDel(items.id)">删除</el-button>
							</div>
						</div>
					</div>
					</template>
				</div>
				</template>
			</div>
		</div>
    </div>
	
    <el-dialog title="添加评论" :visible.sync="dialogFormVisible">
      <el-form :model="form" :rules="rules" ref="form">
        <el-form-item label="评论" label-width="60px" prop="content">
			<editor
			    :style='{"minHeight":"200px","border":"0","outline":"none","color":"#333","borderRadius":"4px","width":"100%","lineHeight":"32px","fontSize":"14px"}'
			    v-model="form.content" 
			    class="editor" 
			    action="file/upload">
			</editor>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">取 消</el-button>
        <el-button type="primary" @click="addForum('form')">确 定</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
  export default {
    //数据集合
    data() {
      return {
        baseUrl: '',
		id: '',
        detail: {},
        commentList: [],
        dialogFormVisible: false,
        form: {
          content: '',
          parentid: '',
          userid: localStorage.getItem('frontUserid'),
          username: localStorage.getItem('username'),
          avatarurl: '',
        },
		userid: localStorage.getItem('frontUserid'),
		editorOption: {
		  modules: {
		    toolbar: [
		      ["bold", "italic", "underline", "strike"],
		      ["blockquote", "code-block"],
		      [{ header: 1 }, { header: 2 }],
		      [{ list: "ordered" }, { list: "bullet" }],
		      [{ script: "sub" }, { script: "super" }],
		      [{ indent: "-1" }, { indent: "+1" }],
		      [{ direction: "rtl" }],
		      [{ size: ["small", false, "large", "huge"] }],
		      [{ header: [1, 2, 3, 4, 5, 6, false] }],
		      [{ color: [] }, { background: [] }],
		      [{ font: [] }],
		      [{ align: [] }],
		      ["clean"],
		      ["image", "video"]
		    ]
		  }
		},
        rules: {
          content: [
            { required: true, message: '请输入评论', trigger: 'blur' }
          ]
        },
		zanType:false,
		zanForm:{},
		showIndex: -1,
		showIndex1: -1,
		allZan: 0
      }
    },
    created() {
		this.baseUrl = this.$config.baseUrl;
		this.id = this.$route.query.id
		this.getDetail()
    },
    //方法集合
    methods: {
		// 返回按钮
		backClick(){
			history.back()
		},
		onEditorReady(editor) {
			editor.root.setAttribute('data-placeholder', "请输入内容...");
		},
		getDetail() {
			this.$http.get(`forum/list/${this.id}`).then(res => {
				if (res.data.code == 0) {
					res.data.data.content = res.data.data.content.replace(/img src/gi,"img style=\"width:100%;\" src");
					this.detail = res.data.data
					this.commentList = res.data.data.childs;
					this.getZan()
					this.getAllZan()
				}
			});
		},
		getZan() {
			this.$http.get('storeup/list', {
				params: {
					page: 1,
					limit: 1,
					type: 21,
					userid: localStorage.getItem('frontUserid'),
					tablename: 'forum',
					refid: this.id
				}
			}).then(res=>{
				if(res.data&&res.data.code==0){
					if(res.data.data.list.length){
						this.zanType = true
						this.zanForm = res.data.data.list[0]
					}else{
						this.zanType = false
					}
				}
			})
		},
		getAllZan() {
			this.$http.get('storeup/list', {
				params: {
					page: 1,
					type: 21,
					tablename: 'forum',
					refid: this.id
				}
			}).then(res => {
				if (res.data && res.data.code == 0) {
					this.allZan = Number(res.data.data.total)
				}
			})
		},
		zanClick() {
			if(this.zanType){
				this.$http.post('storeup/delete', [this.zanForm.id]).then(res => {
					if (res.data && res.data.code == 0) {
						this.$message.success('取消成功')
						this.getZan()
						this.getAllZan()
					}
				})
			}else{
				let data = {
					name: this.detail.title,
					picture: '1',
					refid: this.detail.id,
					type: 21,
					tablename: 'forum',
					userid: localStorage.getItem('frontUserid')
				}
				this.$http.post('storeup/add', data).then(res => {
					if (res.data && res.data.code == 0) {
						this.$message.success('点赞成功')
						this.getZan()
						this.getAllZan()
					}
				})
			}
		},
		// 新增评论
		addComment(){
			this.form.parentid = this.detail.id
			this.dialogFormVisible = true
		},
		// 鼠标移入
		commentEnter(index){
			this.showIndex = index
		},
		// 鼠标移出
		commentLeave(){
			this.showIndex = -1
		},
		// 二级评论鼠标移入
		commentEnter1(index){
			this.showIndex1 = index
		},
		// 二级评论鼠标移出
		commentLeave1(){
			this.showIndex1 = -1
		},
		// 删除评论
		commentDel(id){
			this.$confirm('是否删除此评论？')
			  .then(_ => {
			    this.$http.post('forum/delete',[id]).then(res=>{
				  if(res.data&&res.data.code==0){
					  this.$message({
						type: 'success',
						message: '删除成功!',
						duration: 1500,
						onClose: () => {
							this.getDetail();
						}
					  });
				  }
			  })
		  }).catch(_ => {});
		},
		// 回复评论
		replyClick(id){
			this.form.parentid = id
			this.dialogFormVisible = true
		},
      addForum(formName) {
        this.$refs[formName].validate((valid) => {
          if (valid) {
            this.form.avatarurl = localStorage.getItem('frontHeadportrait')?localStorage.getItem('frontHeadportrait'):'';
            this.$http.post('forum/add', this.form).then(res => {
              if (res.data.code == 0) {
                this.$message({
                  type: 'success',
                  message: '评论成功!',
                  duration: 1500,
                  onClose: () => {
                    this.form.content = '';
                    this.getDetail();
                    this.dialogFormVisible = false;
                  }
                });
              }
            });
          } else {
            return false;
          }
        });
      }
    }
  }
</script>

<style rel="stylesheet/scss" lang="scss" scoped>
	.section {
		width: 900px;
		margin: 0 auto;
	}

	.section-content {
		margin-top: 30px;
	}
	.content-title {
		text-align: center;
		font-size: 22px;
		font-weight: bold;
	}
	.content-sub-title {
		text-align: center;
		margin-top: 20px;
		color: #888888;
		font-size: 14px;
	}
	.clearfix:before,
	.clearfix:after {
		display: table;
		content: "";
	}
	.clearfix:after {
		clear: both
	}
	.header-block {
		height: 50px;
		line-height: 50px;
		display: flex;
	}
	.userinfo {
		align-self: center;
		margin-left: 15px;
	}
	.content-block-ask {
		margin: 15px 0 20px 65px;
	}
	.content-detail img {
		max-width: 900px;
		height: auto;
	}
	.operate{
		display: flex;
		align-items: center;
		justify-content: flex-start;
		padding: 0 40px;
	}
</style>
