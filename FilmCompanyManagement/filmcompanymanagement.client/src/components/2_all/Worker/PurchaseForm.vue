<template>
    <form @submit.prevent="submitPurchase">
        <div>
            <label for="equipmentName">设备名称</label>
            <input type="text" v-model="form.equipmentName" id="equipmentName" />
        </div>
        <div>
            <label for="item">购买项目</label>
            <input type="text" v-model="form.item" id="item" />
        </div>
        <div>
            <label for="amount">费用金额</label>
            <input type="text" v-model="inputAmount" @blur="formatAmount" @input="validateAmount" id="amount" />
        </div>
        <button type="submit">提交</button>
    </form>
</template>

<script>
    import { ref } from 'vue'
	import axios from 'axios';

    export default {
        name: 'PurchaseForm',
        setup() {
            const form = ref({
                equipmentName: '',
                item: '',
                amount: 0
            })

            const userID = this.$route.query.id;

            const inputAmount = ref('')

            const validateAmount = (event) => {
                const value = event.target.value
                event.target.value = value.replace(/[^\d.]/g, '')
            }

            const formatAmount = () => {
                const numericValue = parseFloat(inputAmount.value.replace(/,/g, ''))
                if (!isNaN(numericValue)) {
                    form.value.amount = parseFloat(numericValue.toFixed(2))
                    inputAmount.value = form.value.amount.toLocaleString('en-US', {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    })
                } else {
                    form.value.amount = 0
                    inputAmount.value = ''
                }
            }

            const submitPurchase = async () => {
				// 检查所有字段是否已填写
				if (!form.value.equipmentName || !form.value.item || form.value.amount === 0) {
					alert("请完成所有内容的填写再提交！")
					return
				}

				try {
					console.log('begin 1');
                    axios.post('/api/worker/purchase', {
                        id: userID,
                        equipment: form.value.equipmentName,
                        item: form.value.item,
                        amount: form.value.amount
                    }).then(function (res) {

						if (1) {
							alert("提交成功")
						} else {
							alert("提交失败")
						}
					})
				} catch (error) {
					console.error(error);
					alert("提交失败");
				}
			}

            return { form, inputAmount, validateAmount, formatAmount, submitPurchase }
        }
    }
</script>
