#!/usr/bin/env python
# coding: utf-8

# In[1]:


import pandas as pd

import numpy as np

from matplotlib.pyplot import hist
import matplotlib.pyplot as plt
get_ipython().run_line_magic('matplotlib', 'inline')

import warnings  
warnings.filterwarnings('ignore')


# In[2]:


# Banding rules:

lst_3_normal = ['0.25', '0.75', '1.00']

lst_5_normal = ['0.10', '0.30', '0.70', '0.90', '1.00']

lst_8_normal = ['0.05', '0.15', '0.30', '0.50', '0.70', 
                '0.85', '0.95', '1.00']

lst_13_normal = ['0.02', '0.06', '0.12', '0.20', '0.30', '0.42', 
                 '0.58', '0.70', '0.80', '0.88', '0.94', '0.98', '1.00']

lst_19_normal = ['0.01', '0.03', '0.06', '0.10', '0.15', '0.21', '0.28', 
                 '0.36', '0.45', '0.55', '0.64', '0.72', '0.79', '0.85', 
                 '0.90', '0.94', '0.97', '0.99', '1.00']

lst_5_equal = ['0.20', '0.40', '0.60', '0.80', '1.00']

lst_7_equal = ['0.14', '0.28', '0.42', '0.58','0.72','0.86', '1.00']

lst_10_equal = ['0.10', '0.20', '0.30', '0.40', '0.50', 
                '0.60', '0.70', '0.80', '0.90', '1.00']

lst_20_equal = ['0.05', '0.10', '0.15', '0.20', '0.25', '0.30', 
                '0.35', '0.40', '0.45', '0.50', '0.55', '0.60', '0.65', 
                '0.70', '0.75', '0.80', '0.85', '0.90', '0.95', '1.00']


# In[3]:


def weighted_quantile(values, quantiles, sample_weight=None, 
                      values_sorted=False):
    """ Very close to numpy.percentile, but supports weights.
    NOTE: quantiles should be in [0, 1]!
    :param values: numpy.array with data
    :param quantiles: array-like with many quantiles needed
    :param sample_weight: array-like of the same length as `array`
    :param values_sorted: bool, if True, then will avoid sorting of
        initial array
    :return: numpy.array with computed quantiles.
    """
    values = np.array(values)
    quantiles = np.array(quantiles)
    if sample_weight is None:
        sample_weight = np.ones(len(values))
    sample_weight = np.array(sample_weight)
    assert np.all(quantiles >= 0) and np.all(quantiles <= 1),         'quantiles should be in [0, 1]'

    if not values_sorted:
        sorter = np.argsort(values)
        values = values[sorter]
        sample_weight = sample_weight[sorter]

    weighted_quantiles = np.cumsum(sample_weight) - 0.5 * sample_weight
    weighted_quantiles /= np.sum(sample_weight)
    return np.interp(quantiles, weighted_quantiles, values)

#example:
#weighted_quantile([1, 2, 9, 3.2, 4, 7.2, 6.3, 8.0], [0.0, 0.5, 0.75, 1.])


# In[4]:


data = np.random.uniform(low=1, high=100, size=20).tolist()
weight = np.random.uniform(low=0, high=1, size=20).tolist()

print('input: ', data, '\n')

print('weight: ', weight, '\n')

rule = pd.to_numeric(lst_5_normal)
print('banding rule:', rule,'\n')

one_way_divide = weighted_quantile(data, rule, weight)
print('banding cuts:', one_way_divide,'\n')

print('min: ', min(data), ' max: ', max(data), '\n')

print('Dividing ranges: \n')
for i in range(len(one_way_divide)):
    if i==0: 
        print(i+1, '[{}, {})'.format(min(data),one_way_divide[i]))
    elif i==len(one_way_divide)-1:
        print(i+1, '[{}, {}]'.format(one_way_divide[i-1],one_way_divide[i]))
    else:
        print(i+1, '[{}, {})'.format(one_way_divide[i-1],one_way_divide[i]))


# # One Way for Continuous Variables

# In[5]:


def One_Way_Continuous(df_one_way, target, rule='all', labels={-1:'NA'}, output=False):
    
    """ Calculate One Way for continuous variables.
    NOTE: quantiles should be in [0, 1]!
    :param df_one_way: input dataframe for calculating One Way, it should contain columns "Premium" and "Total Loss"
    :param target: the variable of interest
    :param rule: divide rule, default is 'all', meaning to check all the rules, can be set as '5 normal', etc.
    :param labels: specific values to be listed separately, for example: "NA"
    :return: aggregated results of "Premium", "Total Loss", "LR2" (Loss Ratio), and "LR" (relative Loss Ratio)
    """
    # dividing rules
    divide_name = ['3 normal','5 normal','8 normal','13 normal','19 normal',
                    '5 equal','7 equal','10 equal','20 equal']
    divide_rules = [lst_3_normal,lst_5_normal,lst_8_normal,lst_13_normal,lst_19_normal,
                        lst_5_equal,lst_7_equal,lst_10_equal,lst_20_equal]
    
    # remove NA's - the NA's should have been transfered to numerical, like '-1'
    one_way = df_one_way.loc[(df_one_way[target].isna()==False)]
    
    one_way[['Total Loss','Premium']] = one_way[['Total Loss','Premium']] * 0.001
    LR_mean = df_one_way['Total Loss'].sum()/df_one_way['Premium'].sum()
    
    # use new column 'category' to label different groups
    one_way['category'] = 0
    divide = []
    
    # process the specific categories listed in labels, put each value in a separate group
    cats, remove_lst = [], []
    if labels != {}: 
        cats = list(labels.keys())
        for cat in cats: 
            tmp = one_way.loc[one_way[target]==cat]
            #print('cats:', cats)
            if len(tmp)==0: remove_lst.append(cat)
    for cat in remove_lst:
        cats.remove(cat)
        
    if cats != []:
        one_way['category'] = len(cats)
        for i, cat in enumerate(cats):
            one_way.loc[one_way[target]==cat,'category'] = i
    # process rules
    if rule != 'all': 
        divide_rules = [divide_rules[i] for i in range(len(divide_rules)) if divide_name[i]==rule]
        divide_name = [rule]
        
    # for each rule, calculate One Way and plot the results
    for n in range(len(divide_rules)):
        one_way.loc[one_way.category>=len(cats),'category']=len(cats)
        divide_rule = pd.to_numeric(divide_rules[n])
        divide = list(weighted_quantile(list(one_way.loc[one_way.category==len(cats)][target]), divide_rule, 
                                        sample_weight = list(one_way.loc[one_way.category==len(cats)]['Premium'])))
        divide=sorted(list(set(divide)))
        print('\ndivide: ',divide, '\nmin: ', one_way.loc[one_way.category==len(cats)][target].min(), 
              'max: ', one_way.loc[one_way.category==len(cats)][target].max())
        for i in range(len(divide)-1):
            one_way.loc[(one_way.category>=len(cats))&(one_way[target]>=divide[i]),'category'] = i+1+len(cats)
            
        if divide[0] == one_way.loc[one_way.category>=len(cats)][target].min(): 
            #one_way.loc[one_way[target]==divide[0], 'category'] = len(cats)
            divide = divide[1:]
            
        one_way_tmp = one_way.groupby('category')[['Premium','Total Loss']].sum()
        one_way_tmp['LR2'] = one_way_tmp['Total Loss'] / one_way_tmp['Premium']
        one_way_tmp['LR'] = round(100 * (one_way_tmp.LR2 - LR_mean)/LR_mean)
        one_way_tmp['Premium'] = round(one_way_tmp['Premium'])
        print('Dividing rule: ', divide_name[n], '\n\n', one_way_tmp)
        
        xs = one_way_tmp.index.tolist()
        xs = np.arange(len(one_way_tmp))
        ys = one_way_tmp.LR
        zs = one_way_tmp['Premium']

        # plot the results
        fig = plt.figure(figsize=(12,8))
        ax = fig.add_axes([0, 0.15, 0.9, 0.8])
        ax.plot(xs, ys, marker='o',color='darkorange')
        ax.grid(axis='y',linestyle='--')
        
        plt.axhline(y=0, color='gray',linewidth=1)#, linestyle='-')
        
        # add labels
        for x, y in zip(xs, ys):
            label = str("{:.0f}".format(y)) + '%'
            plt.annotate(label, # this is the text
                         (x,y), # this is the point to label
                         textcoords="offset points", # how to position the text
                         xytext=(0,10), # distance from text to points (x,y)
                         ha='center') # horizontal alignment can be left, right or center

        divide = [str("{:.1f}".format(one_way.loc[one_way.category>=
                                                  len(cats)][target].min()))] + [ str("{:.1f}".format(a)) for a in divide]
        divide = cats + [ '['+divide[i]+'-'+divide[i+1]+')' 
                         for i in range(len(divide)-2)] + [ '['+divide[-2]+'-'+divide[-1]+']']
        
        ax.set_ylabel('Loss Ratio (%)')
        ax.set_ylim(one_way_tmp.LR.min()-0.2*(one_way_tmp.LR.max()-one_way_tmp.LR.min()),
                    one_way_tmp.LR.max()+0.2*(one_way_tmp.LR.max()-one_way_tmp.LR.min()))
        #plt.xticks(np.arange(len(one_way_tmp)),divide)#,rotation=20)
        plt.xticks([])

        # add bar plot of Prem
        ax2 = ax.twinx()
        ax2.bar(xs, zs,edgecolor='black',color='None',fill=False, width=0.5)
        ax2.set_ylim(0,one_way_tmp.loc[one_way_tmp.index>=len(cats)]['Premium'].max()*1.2)
        ax2.set_ylabel('Premium (000s)')
        
        # title
        plt.title(target + ':' + divide_name[n], fontsize = 16)
        
        # add table of data
        tmp = one_way_tmp[['Premium','LR']]
#         print('cats: ',cats,'\ndivide_name: ', divide_name[n], '\nlen of tmp: ', len(tmp),
#               '\ndivide:',divide,'\n',one_way.category.unique(),'\n',one_way_tmp)
        tmp[''] = divide
        tmp.LR = tmp.LR.apply(lambda x: str("{:.0f}".format(x)) + '%')#str(x)+'%')
        tmp['Premium'] = tmp['Premium'].apply(lambda x: str("{:,}".format(int(x))) )
        tmp = tmp[['','Premium','LR']]
        tmp = tmp.T
        the_table = plt.table(cellText=tmp.values, rowLabels=['','Premium (000s)','Relative Loss Ratio'],cellLoc='center')
        the_table.auto_set_font_size(False)
        the_table.set_fontsize(10)
        the_table.scale(1,2)
        for key, cell in the_table.get_celld().items():
            cell.set_linewidth(0)
            
        if output: return one_way_tmp


# # Example

# In[6]:


df = pd.DataFrame(columns=['cont','Premium','Total Loss'])
dt_size = 125
df.cont = np.random.uniform(low=1, high=100, size=dt_size).tolist()
df['Premium'] = np.random.uniform(low=1000, high=2000, size=dt_size).tolist()
df['Total Loss'] = np.random.uniform(low=0, high=1000000, size=dt_size).tolist()

One_Way_Continuous(df,'cont',labels={-1:'NA',0:0},rule='8 normal') #rule='all')
#One_Way_Continuous(df,'cont',rule='all')


# In[7]:


One_Way_Continuous(df,'cont',rule='all')


# # One Way For Categorized Variables

# In[8]:


def One_Way_Categorical(df_one_way, target, output=False,title='',selection=[],labels={}):
    
    """ Calculate One Way for categorical variables. (or banded continuous variables)
    :param df_one_way: input dataframe for calculating One Way, it should contain columns "Premium" and "Total Loss"
    :param target: the variable of interest
    :param title: the title of the chart
    :param selection: a selection of values to calculate One Way 
    :param labels: specific values to be listed separately, for example: "NA"
    :return: aggregated results of "Premium", "Total Loss", "LR_raw" (Loss Ratio), and "LR" (relative Loss Ratio)
    """
    # remove NA (NA should be converted to -1 before plotting)
    one_way = df_one_way.loc[(df_one_way[target].isna()==False)]
    
    # process
    one_way[['Total Loss','Premium']] = one_way[['Total Loss','Premium']] * 0.001
    one_way_tmp = one_way.groupby(target)[['Premium','Total Loss']].sum()
    
    LR_mean = one_way['Total Loss'].sum()/one_way['Premium'].sum()
    LR_mean = df_one_way['Total Loss'].sum()/df_one_way['Premium'].sum()
    
    one_way_tmp['LR_raw'] = one_way_tmp['Total Loss'] / one_way_tmp['Premium']
    one_way_tmp['LR'] = round(100 * (one_way_tmp.LR_raw - LR_mean)/LR_mean)
    one_way_tmp['Premium'] = round(one_way_tmp['Premium'])
    
    one_way_count = one_way.groupby(target)[['Premium']].count()
    one_way_count.columns = ['Count']
    one_way_tmp = pd.merge(one_way_tmp, one_way_count, left_index=True,right_index=True,how='left')
    
    if selection: one_way_tmp = one_way_tmp.loc[selection]

    # plot
    one_way_cat = np.arange(len(one_way_tmp))#one_way_tmp.index.tolist() 
    one_way_LR = one_way_tmp.LR
    one_way_prem = one_way_tmp['Premium']

    fig = plt.figure(figsize=(12,8))
    ax = fig.add_axes([0, 0.15, 0.9, 0.8])
    ax.plot(one_way_cat, one_way_LR, marker='o',color='darkorange')
    ax.grid(axis='y',linestyle='--')
    plt.axhline(y=0, color='gray',linewidth=1)#, linestyle='-')
    
    # axes labels
    for x, y in zip(one_way_cat, one_way_LR):
        label = str("{:.0f}".format(y)) + '%'
        plt.annotate(label, # this is the text
                     (x,y), # this is the point to label
                     textcoords="offset points", # how to position the text
                     xytext=(0,10), # distance from text to points (x,y)
                     ha='center') # horizontal alignment can be left, right or center
    
    ax.set_ylabel('Loss Ratio (%)')
    ax.set_ylim(one_way_tmp.LR.min()-0.2*(one_way_tmp.LR.max()-one_way_tmp.LR.min()),
                one_way_tmp.LR.max()+0.2*(one_way_tmp.LR.max()-one_way_tmp.LR.min()))
    plt.xticks([])
    
    # add bar plot of Prem
    ax2 = ax.twinx()
    ax2.bar(one_way_cat, one_way_prem,edgecolor='black',color='None',fill=False, width=0.35)
    
    ax2.set_ylim(0,one_way_tmp.loc[(one_way_tmp.index!=0)&(one_way_tmp.index!=-1)]['Premium'].max()*1.2)
    ax2.set_ylabel('Prem (000s)')
    
    plt.title(target, fontsize = 16)
    if title: plt.title(title, fontsize = 16)
    
    # add table of premium and relative loss ratio
    tmp = one_way_tmp[['Premium','LR']]
    tmp[''] = one_way_tmp.index.tolist() # x axis labels
    if labels: tmp[''] = [labels[d] if d in labels else d for d in one_way_tmp.index.tolist()]
    tmp.LR = tmp.LR.apply(lambda x: str("{:.0f}".format(x)) + '%')#str(x)+'%')
    tmp['Premium'] = tmp['Premium'].apply(lambda x: str("{:,}".format(int(x))) )
    tmp = tmp[['','Premium','LR']]
    #print(tmp)
    tmp = tmp.T
    
    the_table = plt.table(cellText=tmp.values, rowLabels=['','Prem (000s)','Relative Loss Ratio'],cellLoc='center')
    the_table.auto_set_font_size(False)
    the_table.set_fontsize(12)
    the_table.scale(1,2)
    for key, cell in the_table.get_celld().items():
        cell.set_linewidth(0)
        cell.set_text_props(wrap=True)
    
    if output: return one_way_tmp
    


# # Example

# In[9]:


df = pd.DataFrame(columns=['cat','Premium','Total Loss'])
dt_size = 1000
df.cat = np.random.uniform(low=1, high=10, size=dt_size).tolist()
df.cat = [round(x) for x in df.cat]
print(df.cat.value_counts())
df['Premium'] = np.random.uniform(low=1000, high=2000, size=dt_size).tolist()
df['Total Loss'] = np.random.uniform(low=0, high=1000000, size=dt_size).tolist()
df.head(3)

# if NA exists, fill NA with -1, then add -1: 'NA' to labels:
labels = {1:'label 1',2:'label 2',3:'label 3'}

One_Way_Categorical(df, 'cat',labels=labels, title='One Way for Categorized Variables',output=False,selection=[])


# In[ ]:




