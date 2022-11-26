-- Tencent is pleased to support the open source community by making xLua available.
-- Copyright (C) 2016 THL A29 Limited, a Tencent company. All rights reserved.
-- Licensed under the MIT License (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at
-- http://opensource.org/licenses/MIT
-- Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.

local util = require 'xlua.util'
local message_box = require 'message_box'

-------------------------async_recharge-----------------------------
local function async_recharge(num, cb) --模拟的异步充值
    print('requst server...')
    cb(true, num)
end

local recharge = util.async_to_sync(async_recharge)
-------------------------async_recharge end----------------------------
local buy = function()
    message_box.alert("您余额不足，请充值！", "余额提醒")
	if message_box.confirm("确认充值10元吗？", "确认框") then
		local r1, r2 = recharge(10)
		print('recharge result:', r1, r2)
		message_box.alert("充值成功！", "提示")
	else
	    print('cancel')
	    message_box.alert("取消充值！", "提示")
	end
	print('recharge finished')
end
--将按钮监听点击事件，绑定buy方法
CS.UnityEngine.GameObject.Find("Button"):GetComponent("Button").onClick:AddListener(util.coroutine_call(buy))

